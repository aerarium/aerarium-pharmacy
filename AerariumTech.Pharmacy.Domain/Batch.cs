using System;
using System.Collections.Generic;

namespace AerariumTech.Pharmacy.Domain
{
    public class Batch
    {
        public long Id { get; set; }
        public DateTime DateOfFabrication { get; set; }
        public DateTime DateOfExpiration { get; set; }
        public long ProductId { get; set; }

        public Product Product { get; set; }
        public ICollection<Stock> Stocks { get; set; }
    }
}