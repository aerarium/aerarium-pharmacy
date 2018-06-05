namespace AerariumTech.Pharmacy.App.Models
{
    public class ProductPicture
    {
        public long ProductId { get; set; }
        public byte[] Picture { get; set; }

        public Product Product { get; set; }
    }
}
