using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace srcgen
{

/// <summary>
/// Интервал времени. Атом - секунда.
/// </summary>
public class TimeInerval : IComparable
{
//public:

    /// <summary>
    /// Конструктор от интервала, который представляет собой 1 секунду
    /// </summary>
    /// <param name="begin">Начало в секундах</param>
    public TimeInerval( int begin )
    {
        mBegin = begin;
        mEnd = begin;
    }

    /// <summary>
    /// Конструктор от интервала от - до
    /// </summary>
    /// <param name="begin">Начало в секундах, включительно</param>
    /// <param name="end">Конец в секундах, включительно</param>
    public TimeInerval( int begin, int end )
    {
        mBegin = begin;
        mEnd = end;
        Swap();
    }

    /// <summary>
    /// Начало интервала в секундах
    /// </summary>
    public int Begin
    {
        get
        {
            return mBegin;
        }
        set
        {
            mBegin = value;
            Swap();
        }
    }

    /// <summary>
    /// Конец интервала в секундах
    /// </summary>
    public int End
    {
        get
        {
            return mEnd;
        }
        set
        {
            mEnd = value;
            Swap();
        }
    }

    /// <summary>
    /// Длительность интервала в секундах
    /// </summary>
    /// <returns>Длительность в секундах</returns>
    public int GetLength( )
    {
        if( mBegin == mEnd )
            return 1;
        return ( mEnd - mBegin );
    }

    /// <summary>
    /// Слить два отрезка в один, если они пересекаются
    /// </summary>
    /// <param name="time_interval">Другой отрезок, который подмерживаем к нашему</param>
    /// <returns>True, если наш отрезок его поглотил, False, если они не пересекаются</returns>
    public bool Merge( TimeInerval time_interval )
    {
        if( time_interval == null )
            return false;

        if( !IsIntersect( time_interval ) )
            return false;
        mBegin = Math.Min( mBegin, time_interval.mBegin );
        mEnd = Math.Max( mEnd, time_interval.mEnd );
        return true;
    }

    /// <summary>
    /// Пересекаются ли наш отрезок с этим
    /// </summary>
    /// <param name="time_interval">Отрезок, с которым проверяем на пересечение</param>
    /// <returns>Пересекаются ли отрезки</returns>
    public bool IsIntersect( TimeInerval time_interval )
    {
        if( time_interval == null )
            return false;

        if( mEnd < time_interval.mBegin - 1 )
            return false;
        if( mBegin > time_interval.mEnd + 1 )
            return false;
        return true;
    }

    #region Члены IComparable

    /// <summary>
    /// Сравнение отрезка с другим отрезком (по сути, у кого раньше начало)
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public int CompareTo( object obj )
    {
        if( obj == null )
            return 1;

        if( obj is TimeInerval )
        {
            TimeInerval et = obj as TimeInerval;
            return this.mBegin.CompareTo( et.mBegin );
        }
        return 0;
    }

    #endregion

    public override string ToString( )
    {
        if( mBegin == mEnd )
            return TimeConv.SToHMS( mBegin );
        else
            return TimeConv.SToHMS( mBegin ) + " - " + TimeConv.SToHMS( mEnd );
    }



//private:

    /// <summary>
    /// Начало отрезка
    /// </summary>
    private int mBegin;

    /// <summary>
    /// Конец отрезка
    /// </summary>
    private int mEnd;

    /// <summary>
    /// Поменять начало и конец отрезка местами, если они наоборот
    /// </summary>
    private void Swap()
    {
        if( mBegin > mEnd )
        {
            int tmp = mBegin;
            mBegin = mEnd;
            mEnd = tmp;
        }
    }

}

} //namespace srcgen