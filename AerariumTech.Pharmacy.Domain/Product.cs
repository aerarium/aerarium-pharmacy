using AerariumTech.Pharmacy.Validations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AerariumTech.Pharmacy.Domain
{
    public class Product
    {
        public long Id { get; set; }

        [Display(Name = "Nome")]
        [StringLength(50, ErrorMessage = "Product name must contain between {0} and {1} characters.",
            MinimumLength = 5)]
        [Required(ErrorMessage = "Product name is required.")]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Price(ErrorMessage = "Invalid price.")]
        public decimal Price { get; set; }

        [Price(ErrorMessage = "Invalid price.")]
        public decimal? PriceWithDiscount { get; set; }
        
        public string SerialCode { get; set; }

        public string PathToPicture { get; set; }

        public long SupplierId { get; set; }

        public Supplier Supplier { get; set; }

        [Price]
        public decimal ActualPrice => PriceWithDiscount ?? Price;

        public ICollection<Batch> Batches { get; set; }
        public ICollection<ProductCategory> ProductCategories { get; set; }
        public ICollection<SaleInvoice> SaleInvoices { get; set; }
    }
}