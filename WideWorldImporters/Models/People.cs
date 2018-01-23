using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WideWorldImporters.Models
{
    public class People
    {
        [Required]
        [Key]
        [Column("PersonID")]
        public int PersonId { get; set; }
        [Required]
        [StringLength(50)]
        public string FullName { get; set; }
        [Required]
        [StringLength(50)]
        public string PreferredName { get; set; }
        //[Required]
        //[StringLength(101)]
        //public string SearchName { get; set; }
        [Required]
        [Column(TypeName = "bit")]
        public bool IsPermittedToLogon { get; set; }
        [Required]
        [StringLength(50)]
        public string LogonName { get; set; }
        [Required]
        [Column(TypeName = "bit")]
        public bool IsExternalLogonProvider { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string HashedPassword { get; set; }
        [Required]
        [Column(TypeName = "bit")]
        public bool IsSystemUser { get; set; }
        [Required]
        [Column(TypeName = "bit")]
        public bool IsEmployee { get; set; }
        [Required]
        [Column(TypeName = "bit")]
        public bool IsSalesperson { get; set; }
        public string UserPreferences { get; set; }
        [StringLength(20)]
        public string PhoneNumber { get; set; }
        [StringLength(20)]
        public string FaxNumber { get; set; }
        [StringLength(256)]
        public string EMailAddress { get; set; }
        [Column(TypeName = "varbinary(max)")]
        public string Photo { get; set; }
        public string CustomFields { get; set; }
        //public string OtherLanguages { get; set; }
        [Required]
        public int LastEditedBy { get; set; }
        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime ValidFrom { get; set; }
        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime ValidTo { get; set; }
        public ICollection<Customers> CustomersPrimaryContact { get; set; }
        public ICollection<Customers> CustomersAlternateContact { get; set; }
    }
}
