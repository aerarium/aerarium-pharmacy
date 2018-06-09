namespace AerariumTech.Pharmacy.Domain
{
    /// <summary>
    /// <see cref="Stock"/> is an entity which keeps track of moviment in the batch.
    /// </summary>
    public class Stock
    {
        public long Id { get; set; }
        public long BatchId { get; set; }
        public int Quantity { get; set; }
        public Type Type { get; set; }

        public Batch Batch { get; set; }
    }
}