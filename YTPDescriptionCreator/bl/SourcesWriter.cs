using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace srcgen
{

/// <summary>
/// Умеет распечатывать SoursesInfo по шаблону
/// </summary>
class SourcesWriter
{
// public:

    /// <summary>
    /// Записать файл сурсов
    /// </summary>
    /// <param name="file_name">Путь, по которому файл будет записан</param>
    /// <param name="sources">Список сурсов, которые необходимо записать</param>
    /// <param name="scheme">Схема, по которой список сурсов нужно отформатировать</param>
    /// <returns>true, если успешно</returns>
    public bool WriteFile( string file_name, SoursesInfo sources, Scheme scheme )
    {
        if( scheme == null || sources == null )
            return false;

        List<SoursesInfoPart> list_info = new List<SoursesInfoPart>()
        {
            new SoursesInfoPart
            {
                mSourceType = SourceType.stAUDIO,
                mMediaBlock = scheme.Audio,
                mSourceList = sources.GetListSources( SourceType.stAUDIO )
            },
            new SoursesInfoPart
            {
                mSourceType = SourceType.stVIDEO,
                mMediaBlock = scheme.Video,
                mSourceList = sources.GetListSources( SourceType.stVIDEO )
            }
        };

        using( StreamWriter file = new StreamWriter( file_name, false, Encoding.UTF8 ) )
        {
            file.Write( ReEsc( scheme.TextBegin ) );
            WriteParts( file, list_info );
            file.Write( ReEsc( scheme.TextEnd ) );
        }

        return true;
    }

// private:

    /// <summary>
    /// Информация о том, что записывать и как
    /// </summary>
    private class SoursesInfoPart
    {
        /// <summary>
        /// Список сурсов
        /// </summary>
        public List<Source> mSourceList;

        /// <summary>
        /// Тип сурсов
        /// </summary>
        public SourceType mSourceType;

        /// <summary>
        /// Информация о форматировании списка при записи
        /// </summary>
        public MediaBlock mMediaBlock;
    }

    /// <summary>
    /// Записать список частей в файл
    /// </summary>
    /// <param name="file">поток файла</param>
    /// <param name="list_info">список информаций о частях и как её записывать</param>
    private void WriteParts( StreamWriter file, List<SoursesInfoPart> list_info )
    {
        if( list_info == null )
            return;
        foreach( var elem in list_info )
        {
            if( elem.mSourceList == null
                || elem.mSourceList.Count() == 0
                || elem.mMediaBlock == null
                || !elem.mMediaBlock.Enable
                || elem.mSourceType == SourceType.stUNKNOWN )
                continue;

            WriteList( file, elem.mSourceList, elem.mMediaBlock );
        }
    }


    /// <summary>
    /// Записать список сурсов
    /// </summary>
    /// <param name="file">Поток, в который пишем</param>
    /// <param name="src_list">Список сурсов</param>
    /// <param name="media_block">Информация о форматировании</param>
    private void WriteList( StreamWriter file, List<Source> src_list, MediaBlock media_block )
    {
        if( file == null
            || media_block == null
            || src_list == null
            || src_list.Count() == 0 )
            return;

        file.Write( ReEsc( media_block.Caption ) );
        file.Write( ReEsc( media_block.TableBegin ) );

        bool is_first = true;
        foreach( var src in src_list )
        {
            if( is_first )
                is_first = false;
            else
                file.Write( ReEsc( media_block.TableSeparator ) );

            PrintSource( file, src, media_block );
        }

        file.Write( ReEsc( media_block.TableEnd ) );
    }

    /// <summary>
    /// Записать сурс
    /// </summary>
    /// <param name="file">Поток, в который пишем</param>
    /// <param name="src">Сурс, который пишем</param>
    /// <param name="media_block">Информация о форматировании</param>
    private void PrintSource( StreamWriter file, Source src, MediaBlock media_block )
    {
        if( file == null
            || src == null
            || media_block == null )
            return;

        string times;
        if( media_block.TimesMaxCount > 0
            && src.mListTimeInerval.Count() > media_block.TimesMaxCount
            && media_block.OverTimesMaxCountReplString != null
            && media_block.OverTimesMaxCountReplString != "" )
        {
            times = media_block.OverTimesMaxCountReplString;
        }
        else
        {
            times = src.mListTimeInerval.ToString(
                media_block.TimesMaxCount,
                media_block.TimesSeparator );
        }

        string orig_descr = PathFuncs.FileNameWithoutExt( src.mAddress ).Trim();
        string descr = "";

        if( media_block.DescriptionMaxLine > 0
            && media_block.DescriptionSeparator != null
            && media_block.DescriptionSeparator != "" )
        {
            string sep = media_block.DescriptionSeparator;

            bool is_first = true;
            for(int i=0; i < orig_descr.Length; i+= media_block.DescriptionMaxLine )
            {
                if( is_first )
                    is_first = false;
                else
                    descr += sep;

                if( i + media_block.DescriptionMaxLine < orig_descr.Length )
                    descr += orig_descr.Substring( i, media_block.DescriptionMaxLine );
                else
                    descr += orig_descr.Substring( i );
            }
        }
        else
        {
            descr = orig_descr;
        }

        // Подстановка
        string template = media_block.TableLoopPart;
        template = template.Replace( "{Times}", times );
        template = template.Replace( "{Description}", descr );

        file.Write( ReEsc( template ) );
    }

    /// <summary>
    /// Разэкранировать строку (энтеры и фигурные скобки)
    /// </summary>
    /// <param name="val"></param>
    /// <returns></returns>
    private string ReEsc( string val )
    {
        return BracketsProc( LinesProc( val ) );
    }

    /// <summary>
    /// Замена экранированных переносов строк на настоящие
    /// </summary>
    /// <param name="val">Строка с экранированными переносами строк</param>
    /// <returns>Строка с разэкранированными переносами строк</returns>
    private string LinesProc( string val )
    {
        return val.Replace( "{N}", "\r\n" );
    }

    /// <summary>
    /// Замена экранированных скобок на обычные
    /// </summary>
    /// <param name="val">Строка с экранированными скобками</param>
    /// <returns>Строка с разэкранированными скобками</returns>
    private string BracketsProc( string val )
    {
        return val.Replace( "{{}", "{" ).Replace( "{}}", "}" );
    }
}

} //namespace srcgen
