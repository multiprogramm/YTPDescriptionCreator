using System.IO;

namespace srcgen
{

/// <summary>
/// Функции для работы с путями файлов
/// </summary>
public static class PathFuncs
{
    /// <summary>
    /// Расширение файла в верхнем регистре по пути к нему
    /// </summary>
    /// <param name="path">Путь к файлу</param>
    /// <returns>Расширение (без точки, в верхнем регистре)</returns>
    public static string UpperExtByPath( string path )
    {
        string s_ext = Path.GetExtension( path ).ToUpper();
        s_ext = s_ext.Substring( 1 ); //Убираем точку
        return s_ext;
    }

    /// <summary>
    /// Имя файла (без пути) без расширения по его пути
    /// </summary>
    /// <param name="path">Полный путь к файлу</param>
    /// <returns>Имя файла без расширения</returns>
    public static string FileNameWithoutExt( string path )
    {
        return Path.GetFileNameWithoutExtension( path );
    }
}

}
