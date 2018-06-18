using Microsoft.AspNetCore.Http;

namespace AerariumTech.Pharmacy.Models.ProductsViewModels
{
    public class CreateProductViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal PriceWithoutDiscount { get; set; }
        public string SerialCode { get; set; }
        public long SupplierId { get; set; }
        public IFormFile PictureFile { get; set; }
    }
}