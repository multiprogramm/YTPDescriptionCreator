using System;

namespace srcgen
{

/// <summary>
/// Тип сурса
/// </summary>
public enum SourceType
{
    stUNKNOWN = 0,
    stAUDIO = 1,
    stVIDEO = 2
}

/// <summary>
/// Сурс с временами использования
/// </summary>
public class Source : IComparable
{

    /// <summary>
    /// Создать сурс по локальному адресу
    /// </summary>
    /// <param name="address">Локальный адрес</param>
    public Source( string address )
    {
        mAddress = address;
        mListTimeInerval = new ListTimeInerval();
    }

    /// <summary>
    /// Локальный адрес сурса (на компе)
    /// </summary>
    public string mAddress { get; set; }
    
    /// <summary>
    /// Тип сурса
    /// </summary>
    public SourceType mType { get; set; }

    /// <summary>
    /// Времена использования сурса
    /// </summary>
    public ListTimeInerval mListTimeInerval { get; protected set; }

    #region Члены IComparable

    public int CompareTo( object obj )
    {
        if( obj is Source )
        {
            Source src = obj as Source;

            if( this.mListTimeInerval != null )
                this.mListTimeInerval.SortAndMergeIntervals();
            if( src.mListTimeInerval != null )
                src.mListTimeInerval.SortAndMergeIntervals();

            return this.mListTimeInerval.CompareTo( src.mListTimeInerval );
        }

        return 0;
    }

    #endregion
}
}
