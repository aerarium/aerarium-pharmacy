using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace AerariumTech.Pharmacy.Models.ProductsViewModels
{
    public class CreateProductViewModel
    {
        [Display(Name = "Nome")]
        public string Name { get; set; }

        [Display(Name = "Descrição")]
        public string Description { get; set; }

        [Display(Name = "Preço (com desconto)")]
        public decimal? Price { get; set; }

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