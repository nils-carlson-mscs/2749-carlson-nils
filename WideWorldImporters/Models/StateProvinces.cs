using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace WideWorldImporters.Models
{
    public class StateProvinces
    {
        public StateProvinces() {
            Cities = new HashSet<Cities>();
        }
        
        public int StateProvinceId { get; set; }
        public string StateProvinceCode { get; set; }
        public string StateProvinceName { get; set; }
        public int CountryId { get; set; }
        public string SalesTerritory { get; set; }

        [DisplayFormat(DataFormatString = "{0:n0}", ApplyFormatInEditMode = false)]
        public long? LatestRecordedPopulation { get; set; }
        public int LastEditedBy { get; set; }
        public Countries Country { get; set; }
        public ICollection<Cities> Cities { get; set; }
    }
}
