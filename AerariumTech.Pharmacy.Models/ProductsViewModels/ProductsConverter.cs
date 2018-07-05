using System.Collections.Generic;
using System.Linq;
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
                Price = model.PriceWithoutDiscount,
                PriceWithDiscount = model.Price,
                SerialCode = model.SerialCode,
                ProductCategories = model?.CategoriesId.Select(e => new ProductCategory {CategoryId = e}).ToList(),
                SupplierId = model.SupplierId
                // PathToPicture = model?.PictureFile?.Name
            };

        public static Product Convert(EditProductViewModel model)
            => new Product
            {
                Name = model.Name,
                Description = model.Description,
                Price = model.PriceWithoutDiscount,
                PriceWithDiscount = model.Price,
                SerialCode = model.SerialCode,
                ProductCategories = model.CategoriesIds?.Select(e => new ProductCategory{CategoryId = e}).ToList(),
                SupplierId = model.SupplierId
            };

        public static EditProductViewModel Convert(Product model)
            => new EditProductViewModel
            {
                Name = model.Name,
                Description = model.Description,
                Price = model.PriceWithDiscount,
                PriceWithoutDiscount = model.Price,
                SerialCode = model.SerialCode,
                CategoriesIds = model.ProductCategories?.Select(e => e.CategoryId).ToList(),
                SupplierId = model.SupplierId
            };
    }
}