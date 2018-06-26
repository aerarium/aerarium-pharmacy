using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AerariumTech.Pharmacy.Domain
{
    public class Batch
    {
        public long Id { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateOfFabrication { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateOfExpiration { get; set; }

        public long ProductId { get; set; }

        public Product Product { get; set; }

        public ICollection<Stock> Stocks { get; set; }
    }
}