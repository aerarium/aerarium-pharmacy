using System.ComponentModel.DataAnnotations;

namespace AerariumTech.Pharmacy.Domain
{
    /// <summary>
    /// <see cref="Stock"/> is an entity which keeps track of moviment in the batch.
    /// </summary>
    public class Stock
    {
        public long Id { get; set; }

        [Display(Name = "Lote")]
        public long BatchId { get; set; }

        [Display(Name = "Quantidade")]
        public int Quantity { get; set; }

        [Display(Name = "Movimentação")]
        public MovementType MovementType { get; set; }

        [Display(Name = "Lote")]
        public Batch Batch { get; set; }
    }
}