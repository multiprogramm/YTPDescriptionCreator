using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sony.Vegas;
using srcgen;

namespace srcgen
{
/// <summary>
/// Обёртка над API вегаса
/// </summary>
static class VegWrapper
{
// public:

    /// <summary>
    /// Установить вегас
    /// </summary>
    /// <param name="vegas">вегас</param>
    public static void SetVegas( Vegas vegas )
    {
        if( vegas == null )
            throw new NullReferenceException( "vegas is null" );
        mVegas = vegas;
    }

    /// <summary>
    /// Получить путь к файлу текущего открытого проекта в вегасе
    /// </summary>
    /// <returns></returns>
    public static string GetProjectFilePath( ) { return mVegas.Project.FilePath ?? ""; }

    /// <summary>
    /// Получить начало выделения на таймлайне (округлённое до секунд)
    /// </summary>
    /// <returns></returns>
    public static int StartTime()
    {
        double d_start_time = mVegas.SelectionStart.ToMilliseconds();
        return (int)Math.Round( d_start_time / 1000.0 );
    }

    /// <summary>
    /// Получить конец выделения на таймлайне (округлённое до секунд)
    /// </summary>
    /// <returns></returns>
    public static int EndTime()
    {
        double d_start_time = mVegas.SelectionStart.ToMilliseconds();
        double d_end_time = ( d_start_time + mVegas.SelectionLength.ToMilliseconds() );
        return (int)Math.Round( d_end_time / 1000.0 );
    }

    /// <summary>
    /// Получить словарь сурслистов, разделённых по типам, в которые будут входить сурсы, что попадают в интервал
    /// </summary>
    /// <param name="min_time">Время, с которого нужно начинать просматривать. С 0:00, если null.</param>
    /// <param name="max_time">Время, до которого нужно просматривать сурсы. До конца проекта, если null.</param>
    /// <param name="delta">Приращение в секундах ко всем временам сурсов. 0, если null</param>
    /// <returns></returns>
    public static SoursesInfo GetSourceList(
        List<string> list_video_exts,
        List<string> list_audio_exts,
        int? min_time,
        int? max_time,
        int? delta )
    {
        // Название списка -> Список( адрес, сурс )
        var sources_dicts = new Dictionary<SourceType, Dictionary<string, Source>>();

        foreach( Track track in mVegas.Project.Tracks )
        {
            foreach( TrackEvent track_event in track.Events )
            {
                // Интервал сурса, если входит в наш отрезок
                TimeInerval interval = SourceInterval( track_event, min_time, max_time, delta );
                if( interval == null )
                    continue;

                // Адрес сурса
                string src_file_path = SourceFilePath( track_event );
                if( src_file_path == "" )
                    continue;

                // Если расширение не входит в список нужных, то пропускаем
                SourceType src_type = SourceType.stUNKNOWN;
                string file_ext = PathFuncs.UpperExtByPath( src_file_path );
                if( list_video_exts.Contains( file_ext ) )
                    src_type = SourceType.stVIDEO;
                else if( list_audio_exts.Contains( file_ext ) )
                    src_type = SourceType.stAUDIO;
                else
                    continue;

                Dictionary<string, Source> type_dict;
                if( !sources_dicts.TryGetValue( src_type, out type_dict ) )
                {
                    type_dict = new Dictionary<string, Source>();
                    sources_dicts.Add( src_type, type_dict );
                }

                Source cur_source;
                if( !type_dict.TryGetValue( src_file_path, out cur_source ) )
                {
                    cur_source = new Source( src_file_path );
                    type_dict.Add( src_file_path, cur_source );
                }

                cur_source.mListTimeInerval.AddTime( interval );
            }
        }

        SoursesInfo sources_info = new SoursesInfo( sources_dicts );
        return sources_info;
    }

// private:

    /// <summary>
    /// Извлечь путь к сурсу из ивента
    /// </summary>
    /// <param name="track_event">Ивент</param>
    /// <returns></returns>
    private static string SourceFilePath( TrackEvent track_event )
    {
        if( track_event == null )
            return "";

        // Извлекаем адрес, костыль
        string addr = "";
        try
        {
            addr = track_event.ActiveTake.MediaPath;
        }
        catch { }

        if( addr != "" )
        {
            // Ещё один костыль (типа если начинается с X:, то лежит на жёстком диске, значит имеет адрес)
            if( addr.Length < 2
                || addr[1] != ':'
                || addr[2] != '\\' 
            )
                addr = "";
        }

        return addr;
    }

    /// <summary>
    /// Извлечь правильно сдвинутый интервал времени из ивента
    /// </summary>
    /// <param name="track_event">Вегас-ивент</param>
    /// <param name="min_time">Минимальная граница времени, из которого извлекаем сурсы</param>
    /// <param name="max_time">Максимальная граница времени, из которого извлекаем сурсы</param>
    /// <param name="delta">Пользовательское смещение времени</param>
    /// <returns>Смещённый интервал по сурсу, если он входит в нужное нам время, null, если нет</returns>
    private static TimeInerval SourceInterval(
        TrackEvent track_event,
        int? min_time, int? max_time, int? delta )
    {
        // Реальные времена ивента
        double start_event = track_event.Start.ToMilliseconds() / 1000.0;
        double end_event = track_event.End.ToMilliseconds() / 1000.0;

        if( min_time.HasValue && end_event <= min_time.Value )
            return null;
        if( max_time.HasValue && start_event >= max_time.Value )
            return null;

        // Обрезка согласно диапазону
        if( min_time.HasValue )
            start_event = Math.Max( min_time.Value, start_event );
        if( max_time.HasValue )
            end_event = Math.Min( max_time.Value, end_event );

        int align = 0;
        if( delta.HasValue )
            align = delta.Value - min_time.GetValueOrDefault( 0 );

        int i_start_event = (int)Math.Round( start_event ) + align;
        int i_end_event = (int)Math.Round( end_event ) + align;

        return new TimeInerval( i_start_event, i_end_event );
    }

    /// <summary>
    /// Вегас
    /// </summary>
    private static Vegas mVegas = null;
}
}
