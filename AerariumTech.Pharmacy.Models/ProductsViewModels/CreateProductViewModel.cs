using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AerariumTech.Pharmacy.Validations;
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
        [Price]
        public decimal? Price { get; set; }

        [Display(Name = "Preço")]
        [Price]
        public decimal PriceWithoutDiscount { get; set; }

        [Display(Name = "Código de barras")]
        public string SerialCode { get; set; }

        [Display(Name = "Categorias")]
        public IEnumerable<long> CategoriesId { get; set; }

        [Display(Name = "Fornecedor")]
        public long SupplierId { get; set; }

        [Display(Name = "Imagem")]
        public IFormFile PictureFile { get; set; }
    }
}