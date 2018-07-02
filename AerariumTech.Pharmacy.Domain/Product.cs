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

        [Display(Name = "Descrição")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Display(Name = "Preço")]
        [Price(ErrorMessage = "Invalid price.")]
        public decimal Price { get; set; }

        [Display(Name = "Preço (com desconto)")]
        [Price(ErrorMessage = "Invalid price.")]
        public decimal? PriceWithDiscount { get; set; }
        
        [Display(Name = "Código de barras")]
        public string SerialCode { get; set; }

        [Display(Name = "Imagem")]
        public string PathToPicture { get; set; }

        [Display(Name = "Fornecedor")]
        public long SupplierId { get; set; }

        [Display(Name = "Fornecedor")]
        public Supplier Supplier { get; set; }

        [Price]
        [Display(Name = "Preço")]
        public decimal ActualPrice => PriceWithDiscount ?? Price;

        public ICollection<Batch> Batches { get; set; }
        public ICollection<ProductCategory> ProductCategories { get; set; }
        public ICollection<SaleInvoice> SaleInvoices { get; set; }
    }
}