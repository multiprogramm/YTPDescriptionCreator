using System.Text;

namespace srcgen
{

/// <summary>
/// Помощники для функций работы со временем
/// </summary>
public static class TimeConv
{
//public:

    /// <summary>
    /// Секунды в часы, минуты и секунды
    /// </summary>
    /// <param name="h">Часы</param>
    /// <param name="m">Минуты</param>
    /// <param name="s">Секунды</param>
    /// <param name="time">Из секунд</param>
    static public void SToHMS( out int h, out int m, out int s, int time )
    {
        h = time / SEC_IN_HOUR;
        time %= SEC_IN_HOUR;
        m = time / SEC_IN_MIN;
        time %= SEC_IN_MIN;
        s = time;
    }

    /// <summary>
    /// Перевести количество часов, минут и секунд в секунды
    /// </summary>
    /// <param name="h">Часов</param>
    /// <param name="m">Минут</param>
    /// <param name="s">Секунд</param>
    /// <returns>Всего секунд</returns>
    static public int HMSToS( int h, int m, int s )
    {
        return h * SEC_IN_HOUR + m * SEC_IN_MIN + s;
    }

    /// <summary>
    /// Конвертация времени в секундах в 00:00:00-формат
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    static public string SToHMS( int time )
    {
        StringBuilder result = new StringBuilder( "00:00:00".Length );

        int h, m, s;
        SToHMS( out h, out m, out s, time );

        if( h != 0 )
            result.Append( h ).Append( ':' );

        if( m < 10 && h != 0 )
            result.Append( 0 );
        result.Append( m ).Append( ':' );

        if( s < 10 )
            result.Append( 0 );
        result.Append( s );

        return result.ToString();
    }

//private:

    /// <summary>
    /// Секунд в минуте
    /// </summary>
    private const int SEC_IN_MIN = 60;

    /// <summary>
    /// Секунд в часе
    /// </summary>
    private const int SEC_IN_HOUR = 60 * SEC_IN_MIN;
}

}// namespace srcgen