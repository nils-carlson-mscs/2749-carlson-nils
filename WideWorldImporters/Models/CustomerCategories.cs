using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WideWorldImporters.Models
{
    public class CustomerCategories
    {
        public CustomerCategories()
        {
            //Customers = new HashSet<Customers>();
        }

        [Column("CustomerCategoryID")]
        public int CustomerCategoryId { get; set; }

        [Required]
        [Display(Name = "Cust Category")]
        [StringLength(50)]
        public string CustomerCategoryName { get; set; }

        [Required]
        public int LastEditedBy { get; set; }

        [DataType(DataType.DateTime)]
        [Column(TypeName = "datetime2")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ValidFrom { get; set; }

        [DataType(DataType.DateTime)]
        [Column(TypeName = "datetime2")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ValidTo { get; set; }

        //public ICollection<Customers> Customers { get; set; }
    }
}
