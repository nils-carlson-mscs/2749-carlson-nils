using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WideWorldImporters.Models
{
    public class Customers
    {
        [Required]
        [Key]
        [Column("CustomerID")]
        public int CustomerId { get; set; }
        [Required]
        [StringLength(100)]
        public string CustomerName { get; set; }
        [Required]
        [Column("BillToCustomerID")]
        public int BillToCustomerId { get; set; }
        [Required]
        [Column("CustomerCategoryID")]
        public int CustomerCategoryId { get; set; }
        [Column("BuyingGroupID")]
        public int BuyingGroupId  { get; set; }
        [Required]
        [Column("PrimaryContactPersonID")]
        public int PrimaryContactPersonId { get; set; }
        [Column("AlternateContactPersonID")]
        public int AlternateContactPersonId { get; set; }
        [Required]
        [Column("DeliveryMethodID")]
        public int DeliveryMethodId  { get; set; }
        [Required]
        [Column("DeliveryCityID")]
        public int DeliveryCityId { get; set; }
        [Required]
        [Column("PostalCityID")]
        public int PostalCityId { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? CreditLimit { get; set; }
        [Required]
        [Column(TypeName = "date")]
        public DateTime AccountOpenedDate { get; set; }
        [Required]
        [Column(TypeName = "decimal(18,3)")]
        public decimal StandardDiscountPercentage { get; set; }
        [Required]
        [Column(TypeName = "bit")]
        public bool IsStatementSent { get; set; }
        [Required]
        [Column(TypeName = "bit")]
        public bool IsOnCreditHold { get; set; }
        [Required]
        public int PaymentDays { get; set; }
        [Required]
        [StringLength(20)]
        public string PhoneNumber { get; set; }
        [Required]
        [StringLength(20)]
        public string FaxNumber { get; set; }
        [Required]
        [StringLength(5)]
        public string DeliveryRun { get; set; }
        [StringLength(5)]
        public string RunPosition { get; set; }
        [Required]
        [StringLength(256)]
        public string WebsiteURL { get; set; }
        [Required]
        [StringLength(60)]
        public string DeliveryAddressLine1 { get; set; }
        [StringLength(60)]
        public string DeliveryAddressLine2 { get; set; }
        [Required]
        [StringLength(10)]
        public string DeliveryPostalCode { get; set; }
        //[Column(TypeName = "geography")]
        [StringLength(50)]
        public string DeliveryLocation { get; set; }
        [Required]
        [StringLength(60)]
        public string PostalAddressLine1 { get; set; }
        [StringLength(60)]
        public string PostalAddressLine2 { get; set; }
        [Required]
        [StringLength(10)]
        public string PostalPostalCode { get; set; }
        [Required]
        public int LastEditedBy { get; set; }
        [Required]
        [Column(TypeName = "datetime2(7)")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ValidFrom { get; set; }
        [Required]
        [Column(TypeName = "datetime2(7)")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ValidTo { get; set; }

        [ForeignKey("CustomerCategoryId")]
        public CustomerCategories CustomerCategory { get; set; }

        public Cities PostalCity { get; set; }
        public Cities DeliveryCity { get; set; }
        public People PrimaryContactPerson { get; set; }
        public People AlternateContactPerson { get; set; }
    }
}
