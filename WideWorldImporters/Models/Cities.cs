using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace WideWorldImporters.Models
{
    public class Cities
    {
        public Cities() { }

        public int CityId { get; set; }
        public string CityName { get; set; }
        public int StateProvinceId { get; set; }

        [DisplayFormat(DataFormatString = "{0:n0}", ApplyFormatInEditMode = false)]
        public long? LatestRecordedPopulation { get; set; }
        public int LastEditedBy { get; set; }
        public StateProvinces StateProvince { get; set; }

        public ICollection<Customers> CustomersPostal { get; set; }
        public ICollection<Customers> CustomersDelivery { get; set; }
    }
}
