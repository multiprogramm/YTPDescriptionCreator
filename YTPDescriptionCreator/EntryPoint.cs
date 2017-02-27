using Sony.Vegas;
using System.Windows.Forms;

namespace srcgen
{

/// <summary>
/// Класс-точка входа
/// </summary>
public class EntryPoint
{
    /// <summary>
    /// Этот метод вызовется из Sony Vegas
    /// </summary>
    public void FromVegas( Vegas vegas )
    {
        VegWrapper.SetVegas( vegas );

        // Запустим форму приложения
        Application.EnableVisualStyles();
        Form options_form = new OptionsForm();
        options_form.ShowDialog();
    }
}

} //namespace srcgen
