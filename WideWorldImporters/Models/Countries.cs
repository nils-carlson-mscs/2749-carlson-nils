using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace WideWorldImporters.Models
{
    public class Countries
    {
        public Countries()
        {
            StateProvinces = new HashSet<StateProvinces>();
        }
        
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public string FormalName { get; set; }
        public string IsoAlpha3Code { get; set; }
        public int? IsoNumericCode { get; set; }
        public string CountryType { get; set; }

        [DisplayFormat(DataFormatString = "{0:n0}", ApplyFormatInEditMode = false)]
        public long? LatestRecordedPopulation { get; set; }
        public string Continent { get; set; }
        public string Region { get; set; }
        public string Subregion { get; set; }
        public int LastEditedBy { get; set; }
        //public DateTime ValidFrom { get; set; }
        //public DateTime ValidTo { get; set; }

        //public People LastEditedByNavigation { get; set; }
        public ICollection<StateProvinces> StateProvinces { get; set; }
    }
}
