using AerariumTech.Pharmacy.Validations;
using Microsoft.AspNetCore.Http;

namespace AerariumTech.Pharmacy.Models.ProductsViewModels
{
    public class EditProductViewModel
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        [Price]
        public decimal? Price { get; set; }
        
        [Price]
        public decimal PriceWithoutDiscount { get; set; }

        public string SerialCode { get; set; }

        public long SupplierId { get; set; }

        public IFormFile PictureFile { get; set; }
    }
}
