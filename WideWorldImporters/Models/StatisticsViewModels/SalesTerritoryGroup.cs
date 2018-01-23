using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace WideWorldImporters.Models.StatisticsViewModels
{
    public class SalesTerritoryGroup
    {
        [Key]
        public string SalesTerritory { get; set; }
        public int TerritoryStateCount { get; set; }

        [DisplayFormat(DataFormatString = "{0:n0}", ApplyFormatInEditMode = false)]
        public long? TerritorySumPopulation { get; set; }
    }
}
