namespace AerariumTech.Pharmacy.App.Models
{
    public sealed class ProductCategory
    {
        public long ProductId { get; set; }
        public long CategoryId { get; set; }
        
        public Product Product { get; set; }
        public Category Category { get; set; }
    }
}