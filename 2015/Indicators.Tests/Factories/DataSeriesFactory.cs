using common;
using commonClass;
// <copyright file="DataSeriesFactory.cs" company="cesti">Copyright © cesti 2011</copyright>

using System;
using Microsoft.Pex.Framework;

namespace commonClass
{
    /// <summary>A factory for commonClass.DataSeries instances</summary>
    public static partial class DataSeriesFactory
    {
        /// <summary>A factory for commonClass.DataSeries instances</summary>
        [PexFactoryMethod(typeof(DataSeries))]
        public static DataSeries Create(
            DataBars ds_dataBars,
            string _name_s,
            int FirstValidValue_i,
            string Name_s1,
            DictionaryList Cache_dictionaryList
        )
        {
            DataSeries dataSeries = new DataSeries(ds_dataBars, _name_s);
            dataSeries.FirstValidValue = FirstValidValue_i;
            dataSeries.Name = Name_s1;
            dataSeries.Cache = Cache_dictionaryList;
            return dataSeries;

            // TODO: Edit factory method of DataSeries
            // This method should be able to configure the object in all possible ways.
            // Add as many parameters as needed,
            // and assign their values to each field by using the API.
        }
    }
}
