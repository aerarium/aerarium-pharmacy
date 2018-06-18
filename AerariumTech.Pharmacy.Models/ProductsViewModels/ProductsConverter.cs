using AerariumTech.Pharmacy.Domain;

namespace AerariumTech.Pharmacy.Models.ProductsViewModels
{
    public static class ProductsConverter
    {
        public static Product Convert(CreateProductViewModel model)
            => new Product
            {
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
                PriceWithDiscount = model.PriceWithoutDiscount,
                SerialCode = model.SerialCode,
                SupplierId = model.SupplierId
                // PathToPicture = model?.PictureFile?.Name
            };
    }
}
