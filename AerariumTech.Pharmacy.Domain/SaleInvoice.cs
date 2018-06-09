namespace AerariumTech.Pharmacy.Domain
{
    public class SaleInvoice
    {
        public long SaleId { get; set; }
        public long ProductId { get; set; }
        public int Quantity { get; set; }

        public Sale Sale { get; set; }
        public Product Product { get; set; }
    }
}