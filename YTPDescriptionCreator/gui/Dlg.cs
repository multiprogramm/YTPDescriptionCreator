using System.Windows.Forms;

namespace srcgen
{

/// <summary>
/// Отображалка модальных диалогов для пользователя
/// </summary>
public static class Dlg
{
    /// <summary>
    /// Вывод пользователю сообщения об ошибке
    /// </summary>
    /// <param name="text">Текст ошибки</param>
    static public void Error( string text )
    {
        MessageBox.Show( text, ThisProgram.GetTitle(), MessageBoxButtons.OK, MessageBoxIcon.Error );
    }

    /// <summary>
    /// Вывод пользователю диалога сохранения файла
    /// </summary>
    /// <param name="default_file_name">Путь по умолчанию, может быть пустой строкой</param>
    /// <param name="filter">Фильтр (по правилам диалогов)</param>
    /// <returns>Пустрая строка, если пользователь передумал, путь к файлу, если ок</returns>
    static public string Save( string default_file_name, string filter )
    {
        using( SaveFileDialog save_file_dlg = new SaveFileDialog() )
        {
            if( default_file_name != "" )
                save_file_dlg.FileName = default_file_name;

            save_file_dlg.AddExtension = true;
            save_file_dlg.Filter = filter;

            DialogResult dlg_res = save_file_dlg.ShowDialog();
            if( dlg_res != DialogResult.OK )
                return "";
            return save_file_dlg.FileName;
        }
    }

}

} // namespace srcgen