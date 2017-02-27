using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;

namespace srcgen
{

/// <summary>
/// Опции работы
/// </summary>
[XmlRootAttribute]
public class Options
{
    [XmlAttribute]
    public string ProgVer;

    /// <summary>
    /// Список схем
    /// </summary>
    public List<Scheme> Schemes { get; set; }

    public List<Scheme> GetEnabledShemes( )
    {
        List<Scheme> result = new List<Scheme>();

        foreach( var sh in Schemes )
            if( sh.Enable )
                result.Add( sh );

        return result;
    }

    /// <summary>
    /// Получить форматы по-умолчанию (захардкоженные)
    /// </summary>
    /// <returns>Информация о схемах</returns>
    public static Options GetStandart()
    {
        Options print_options = new Options()
        {
            Schemes = new List<Scheme>()
            {
                Scheme.GetSimpleTxtScheme(),
                Scheme.GetWikiVkScheme(),
                Scheme.GetXmlScheme()
            },
            ProgVer = ThisProgram.GetVersion()
        };

        return print_options;
    }

    /// <summary>
    /// Сохранить форматы в XML файл
    /// </summary>
    /// <param name="file_path">Путь к файлу</param>
    public void Save( string file_path )
    {
        ProgVer = ThisProgram.GetVersion();

        XmlSerializer serializer = new XmlSerializer( typeof( Options ) );
        using( FileStream fs = new FileStream( file_path, FileMode.OpenOrCreate ) )
            serializer.Serialize( fs, this );
    }

    /// <summary>
    /// Загрузить форматы из XML файла
    /// </summary>
    /// <param name="file_path">Путь к файлу</param>
    /// <returns>Информация о схемах</returns>
    static public Options Load( string file_path )
    {
        Options result = null;

        try
        {
            XmlSerializer serializer = new XmlSerializer( typeof( Options ) );
            using( FileStream fs = new FileStream( file_path, FileMode.Open ) )
                result = (Options)serializer.Deserialize( fs );
        }
        catch
        {
            result = null;
        }

        return result;
    }
}

/// <summary>
/// Схема выгрузки - один формат файла для сохранения сурслиста
/// </summary>
[Serializable]
public class Scheme
{
    /// <summary>
    /// Название схемы
    /// </summary>
    [XmlAttribute]
    public string Name { get; set; }

    /// <summary>
    /// Используется ли
    /// </summary>
    [XmlAttribute]
    public bool Enable { get; set; }

    /// <summary>
    /// Расширение файла
    /// </summary>
    public string FileExtension { get; set; }
    
    /// <summary>
    /// Данные об аудио
    /// </summary>
    public MediaBlock Audio { get; set; }
    
    /// <summary>
    /// Данные о видео
    /// </summary>
    public MediaBlock Video { get; set; }
    
    /// <summary>
    /// Вступление
    /// </summary>
    public string TextBegin { get; set; }
    
    /// <summary>
    /// Завершение
    /// </summary>
    public string TextEnd { get; set; }





    /// <summary>
    /// Получить схему простого описания (например, для вставки в описание к видео на ютубе)
    /// </summary>
    /// <returns>Схема простого описания</returns>
    public static Scheme GetSimpleTxtScheme( )
    {
        Scheme sheme = new Scheme()
        {
            Name = "Простое описание",
            Enable = true,
            FileExtension = "txt",
            TextBegin = "--- Авторские комментарии ---{N}{N}",
            Audio = new MediaBlock()
            {
                Enable = true,
                Extensions = MediaBlock.DefaultAudioExtensions(),
                Caption = "--- Аудио ---{N}",

                TableBegin = "",

                TableLoopPart = "{Times} {Description}{N}",

                TimesMaxCount = 3,
                TimesSeparator = ",{N}",
                OverTimesMaxCountReplString = "",

                DescriptionMaxLine = 0,
                DescriptionSeparator = "",

                TableSeparator = "",

                TableEnd = "{N}{N}"
            },
            Video = new MediaBlock()
            {
                Enable = true,
                Extensions = MediaBlock.DefaultVideoExtensions(),
                Caption = "--- Видео ---{N}",

                TableBegin = "",

                TableLoopPart = "{Times} {Description}{N}{N}",

                TimesMaxCount = 3,
                TimesSeparator = ",{N}",
                OverTimesMaxCountReplString = "[Основной сурс]",

                DescriptionMaxLine = 0,
                DescriptionSeparator = "",

                TableSeparator = "{N}",

                TableEnd = "{N}{N}"
            },
            TextEnd = ""
        };

        return sheme;
    }

    /// <summary>
    /// Получить схему вики-страницы для вконтакта
    /// </summary>
    /// <returns>Схема вики-вк</returns>
    public static Scheme GetWikiVkScheme( )
    {
        Scheme sheme = new Scheme()
        {
            Name = "Вк-вики",
            Enable = true,
            FileExtension = "txt",
            TextBegin = "==Авторские комментарии=={N}{N}{N}{N}",
            Audio = new MediaBlock()
            {
                Enable = true,
                Extensions = MediaBlock.DefaultAudioExtensions(),
                Caption = "==Аудио=={N}",

                TableBegin = "{|{N}",

                TableLoopPart = "| {Times}{N}| {Description}<br>{N}",

                TimesMaxCount = 3,
                TimesSeparator = "<br>",
                OverTimesMaxCountReplString = "",

                DescriptionMaxLine = 50,
                DescriptionSeparator = "<br>",

                TableSeparator = "|-{N}",

                TableEnd = "|}{N}{N}"
            },
            Video = new MediaBlock()
            {
                Enable = true,
                Extensions = MediaBlock.DefaultVideoExtensions(),
                Caption = "==Видео=={N}",

                TableBegin = "{|{N}",

                TableLoopPart = "| {Times}{N}| [|{Description}]{N}",

                TimesMaxCount = 3,
                TimesSeparator = "<br>",
                OverTimesMaxCountReplString = "Основной сурс",

                DescriptionMaxLine = 50,
                DescriptionSeparator = "<br>",

                TableSeparator = "|-{N}",

                TableEnd = "|}{N}{N}"
            },
            TextEnd = "Подсасывайся на наш конал"
        };

        return sheme;
    }

    /// <summary>
    /// Получить схему псевдохмл
    /// </summary>
    /// <returns>Схема для выгрузки в XML</returns>
    public static Scheme GetXmlScheme( )
    {
        Scheme sheme = new Scheme()
        {
            Name = "Xml",
            Enable = false,
            FileExtension = "xml",
            TextBegin = "<Description>{N}",
            Audio = new MediaBlock()
            {
                Enable = true,
                Extensions = MediaBlock.DefaultAudioExtensions(),
                Caption = "",

                TableBegin = "  <Audio>{N}",

                TableLoopPart = ""
                    + "    <Source>{N}"
                    + "      <Times>{Times}</Times>{N}"
                    + "      <Description>{Description}</Description>{N}"
                    + "    </Source>{N}",

                TimesMaxCount = -1,
                TimesSeparator = ", ",
                OverTimesMaxCountReplString = "",

                DescriptionMaxLine = 0,
                DescriptionSeparator = "",

                TableSeparator = "",

                TableEnd = "  </Audio>{N}"
            },
            Video = new MediaBlock()
            {
                Enable = true,
                Extensions = MediaBlock.DefaultVideoExtensions(),
                Caption = "",

                TableBegin = "  <Video>{N}",

                TableLoopPart = ""
                    + "    <Source>{N}"
                    + "      <Times>{Times}</Times>{N}"
                    + "      <Description>{Description}</Description>{N}"
                    + "    </Source>{N}",

                TimesMaxCount = -1,
                TimesSeparator = ", ",
                OverTimesMaxCountReplString = "",

                DescriptionMaxLine = 0,
                DescriptionSeparator = "",

                TableSeparator = "",

                TableEnd = "  </Video>{N}"
            },
            TextEnd = "</Description>"
        };

        return sheme;
    }
}

/// <summary>
/// Данные о медиаблоке (видео или аудио)
/// </summary>
public class MediaBlock
{
    /// <summary>
    /// Используется ли
    /// </summary>
    [XmlAttribute]
    public bool Enable { get; set; }
    
    /// <summary>
    /// Расширения файлов, которые считаются принадлежащими к этому медиаблоку
    /// </summary>
    public List<string> Extensions { get; set; }

    /// <summary>
    /// Заголовок медиаблока
    /// </summary>
    public string Caption { get; set; }

    /// <summary>
    /// Начало таблицы (списка)
    /// </summary>
    public string TableBegin { get; set; }

    /// <summary>
    /// Повторяющаяся часть
    /// </summary>
    public string TableLoopPart { get; set; }

    /// <summary>
    /// Разделитель
    /// </summary>
    public string TableSeparator { get; set; }

    /// <summary>
    /// Максимальное число времён
    /// </summary>
    public int TimesMaxCount { get; set; }

    /// <summary>
    /// Если превышено максимальное число времён (интервалов), то
    /// вместо интервалов будет записан этот текст
    /// </summary>
    public string OverTimesMaxCountReplString { get; set; }

    /// <summary>
    /// Разделитель между интервалами
    /// </summary>
    public string TimesSeparator { get; set; }

    /// <summary>
    /// Максимальное количество символов для описания. Если оно превышается, то
    /// описание дробится на строки не больше этого размера, разделённые DescriptionSeparator
    /// </summary>
    public int DescriptionMaxLine { get; set; }

    /// <summary>
    /// см. DescriptionMaxLine
    /// </summary>
    public string DescriptionSeparator { get; set; }

    /// <summary>
    /// Конец таблицы
    /// </summary>
    public string TableEnd { get; set; }


    /// <summary>
    /// Расширения
    /// </summary>
    public List<string> GetExtensions()
    {
        List<string> result = new List<string>();
        foreach( var ext in Extensions )
            result.Add( ext.Trim().ToUpper() );
        return result;
    }


    /// <summary>
    /// Расширения аудиофайлов по умолчанию
    /// </summary>
    /// <returns></returns>
    static public List<string> DefaultAudioExtensions( )
    {
        string list = "AA3,AIF,AU,BWF,CDA,DIG,FLAC,MP3,M4A,OGG,OMA,PCA,SND,VOX,W64,WAV,WMA";
        return list.Split( new char[] { ',' } ).ToList();
    }

    /// <summary>
    /// Расширения видеофайлов по умолчанию
    /// </summary>
    /// <returns></returns>
    static public List<string> DefaultVideoExtensions( )
    {
        string list = "AAF,ASF,AVC,AVI,DLX,DV,M2T,M2TS,MOV,MP4,MPEG,MPG,QT,R3D,SWF,WMV,VEG";
        return list.Split( new char[] { ',' } ).ToList();
    }
}

} // namespace srcgen
