using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace srcgen
{

// Информация о сурсах
class SoursesInfo
{
// public:
    public SoursesInfo( Dictionary<SourceType, Dictionary<string, Source>> sources_dicts )
    {
        mDictByType = new Dictionary<SourceType, List<Source>>();

        if( sources_dicts != null )
        {
            foreach( var pair in sources_dicts )
            {
                SourceType src_type = pair.Key;
                var dict_src = pair.Value;

                List<Source> source_list;
                if( dict_src != null )
                {
                    source_list = dict_src.Values.ToList<Source>();
                    source_list.Sort();
                }
                else
                    source_list = new List<Source>();

                mDictByType[src_type] = source_list;
            }
        }

        if( !mDictByType.ContainsKey( SourceType.stAUDIO ) )
            mDictByType.Add( SourceType.stAUDIO, new List<Source>() );

        if( !mDictByType.ContainsKey( SourceType.stVIDEO ) )
            mDictByType.Add( SourceType.stVIDEO, new List<Source>() ); 
    }

    /// <summary>
    /// Получить список сурсов по типу
    /// </summary>
    /// <param name="source_type">Тип сурсов</param>
    /// <returns>Список сурсов</returns>
    public List<Source> GetListSources( SourceType source_type )
    {
        if( source_type == SourceType.stUNKNOWN )
            return null;
        return mDictByType[source_type];
    }

    public bool SaveInFile( string file_path, Scheme scheme )
    {
        SourcesWriter writer = new SourcesWriter();
        return writer.WriteFile( file_path, this, scheme );
    }

// private:

    /// <summary>
    /// Списки сурсов по типу
    /// </summary>
    Dictionary<SourceType, List<Source>> mDictByType;
}

}
