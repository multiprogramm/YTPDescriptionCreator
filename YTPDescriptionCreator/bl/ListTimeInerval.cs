using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace srcgen
{

/// <summary>
/// Список интервалов
/// </summary>
public class ListTimeInerval : IComparable
{
// public:

    /// <summary>
    /// Добавить интервал использования сурса
    /// </summary>
    /// <param name="time_interval">Интервал использования сурса</param>
    public void AddTime( TimeInerval time_interval )
    {
        if( time_interval != null )
        {
            mIntervalList.Add( time_interval );
            mIsIntervalListSorted = false;
        }
    }

    /// <summary>
    /// Сортировка и слияние времён
    /// </summary>
    public void SortAndMergeIntervals( )
    {
        if( mIsIntervalListSorted )
            return;

        // Сначала просто отсортируем (по первой координате)
        mIntervalList.Sort();

        // Теперь помержим отрезки. На каждой итерации
        // Предыдущий отрезок может "поглотить" следующий,
        // если он с ним пересекается
        for( int i = 1 ; i < mIntervalList.Count() ; )
        {
            if( mIntervalList[i - 1].Merge( mIntervalList[i] ) )
                mIntervalList.RemoveAt( i );
            else
                i++;
        }

        // Теперь лист отсортирован и отрезки в нём не пересекаются
        mIsIntervalListSorted = true;
    }
    
    /// <summary>
    /// Посчитать суммарное число секунд
    /// </summary>
    public int GetSumLength()
    {
        SortAndMergeIntervals();
        int result = 0;

        foreach( var v in mIntervalList )
            result += v.GetLength();
        return result;
    }

    /// <summary>
    /// Посчитать максимальную длительность фрагмента
    /// </summary>
    public int GetMaxLength( )
    {
        SortAndMergeIntervals();
        int result = 0;

        foreach( var v in mIntervalList )
        {
            int cur = v.GetLength();
            if( result < cur )
                result = cur;
        }

        return result;
    }

    /// <summary>
    /// Количество интервалов
    /// </summary>
    public int Count( )
    {
        SortAndMergeIntervals();
        return mIntervalList.Count();
    }


    #region Члены IComparable

    /// <summary>
    /// Сравнить с другим списком интервалов (по сути, у кого есть более ранний интервал)
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public int CompareTo( object obj )
    {
        ListTimeInerval oth_list = obj as ListTimeInerval;
        if( oth_list == null )
            return -1;

        if( this.mIntervalList.Count == 0 && oth_list.mIntervalList.Count == 0 )
            return 0;
        else if( this.mIntervalList.Count == 0 )
            return 1;
        else if( oth_list.mIntervalList.Count == 0 )
            return -1;

        this.SortAndMergeIntervals();
        oth_list.SortAndMergeIntervals();

        if( this.mIntervalList[0].Begin > oth_list.mIntervalList[0].Begin )
            return 1;
        else if( this.mIntervalList[0].Begin < oth_list.mIntervalList[0].Begin )
            return -1;

        return 0;
    }

    #endregion

    /// <summary>
    /// Вывести список интервалов в строковом виде
    /// </summary>
    /// <param name="max_lines">Сколько максимум строк-времён выводим.
    /// Если положительное число, то количество.
    /// Если отрицательные - то нет ограничения.</param>
    /// <param name="separator">Разделитель между интервалами</param>
    public string ToString( int max_lines, string separator )
    {
        SortAndMergeIntervals();

        // Количество интервалов
        int cnt_intervals = mIntervalList.Count();
        if( max_lines >= 0 && cnt_intervals > max_lines )
            cnt_intervals = max_lines;
        
        // Количество разделителей
        int cnt_separator = cnt_intervals - 1;
        if( cnt_separator < 0 )
            cnt_separator = 0;

        // Сколько символов максимум нужно
        int symb_max = cnt_intervals * MAX_CHARS_BY_ONE_INTERVAL + separator.Length * cnt_separator;

        StringBuilder result = new StringBuilder( symb_max );

        bool is_need_check_count = ( max_lines <= 0 );

        bool is_first = true;
        foreach( var cur_int in mIntervalList )
        {
            if( is_first )
                is_first = false;
            else
                result.Append( separator );
            result.Append( cur_int.ToString() );

            if( is_need_check_count )
            {
                max_lines--;
                if( max_lines == 0 )
                    break;
            }
        }
        return result.ToString();
    }

// private:

    /// <summary>
    /// Список интервалов
    /// </summary>
    private List<TimeInerval> mIntervalList = new List<TimeInerval>();
    
    /// <summary>
    /// Отсортированы/помержены ли интервалы
    /// </summary>
    private bool mIsIntervalListSorted = true;

    /// <summary>
    /// Максимальное количество символов на интервал
    /// </summary>
    private readonly int MAX_CHARS_BY_ONE_INTERVAL = "00:00:00 - 00:00:00".Length;
}

}
