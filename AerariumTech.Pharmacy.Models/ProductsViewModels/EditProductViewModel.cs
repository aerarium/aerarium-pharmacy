using AerariumTech.Pharmacy.Validations;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AerariumTech.Pharmacy.Models.ProductsViewModels
{
    public class EditProductViewModel
    {
        public long Id { get; set; }

        [Display(Name = "Nome")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Price]
        [Display(Name = "Preço (com desconto)")]
        public decimal? Price { get; set; }
        
        [Price]
        [Display(Name = "Preço")]
        public decimal PriceWithoutDiscount { get; set; }

        [Display(Name = "Código de barras")]
        public string SerialCode { get; set; }
        
        [Display(Name = "Categorias")]
        public IEnumerable<long> CategoriesIds { get; set; }

        [Display(Name = "Fornecedor")]
        public long SupplierId { get; set; }

        [Display(Name = "Imagem")]
        public IFormFile PictureFile { get; set; }
    }
}
