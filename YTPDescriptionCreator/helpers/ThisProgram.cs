using System;
using System.Reflection;
using System.IO;

namespace srcgen
{

/// <summary>
/// Информация об этой программе
/// </summary>
static class ThisProgram
{
// public:

    /// <summary>
    /// Заголовок
    /// </summary>
    /// <returns>Заголовок</returns>
    public static string GetTitle( ) { return GetAssemblyAttribute<AssemblyTitleAttribute>( a => a.Title ); }
    
    /// <summary>
    /// Автор
    /// </summary>
    /// <returns>Автор</returns>
    public static string GetCopyright( ) { return GetAssemblyAttribute<AssemblyCopyrightAttribute>( a => a.Copyright ); }
    
    /// <summary>
    /// Версия
    /// </summary>
    /// <returns>Версия</returns>
    public static string GetVersion( )
    {
        Assembly assem = Assembly.GetExecutingAssembly();
        AssemblyName assemName = assem.GetName();
        Version ver = assemName.Version;
        return ver.ToString();
    }


    public static string GetOptionsFilePath( )
    {
        string dll_path = Assembly.GetExecutingAssembly().Location;
        string dll_folder_path = Path.GetDirectoryName( dll_path );
        return Path.Combine( dll_folder_path, OPTIONS_FILE_NAME );
    }

    public static string PngIcoPath()
    {
        string dll_path = Assembly.GetExecutingAssembly().Location;
        return dll_path + ".png";
    }

    public static string GetVkPageLink()
    {
        return VK_PAGE;
    }

// private:

    /// <summary>
    /// Получение значения параметра сборки по его типу
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    private static string GetAssemblyAttribute<T>( Func<T, string> value )
         where T : Attribute
    {
        T attribute = (T)Attribute.GetCustomAttribute( Assembly.GetExecutingAssembly(), typeof( T ) );
        return value.Invoke( attribute );
    }

    /// <summary>
    /// Название файла настроек
    /// </summary>
    private const string OPTIONS_FILE_NAME = @"YTPDescriptionCreator.xml";

    /// <summary>
    /// Группа вк
    /// </summary>
    private const string VK_PAGE = @"https://vk.com/club134523887";
}

} //namespace srcgen
