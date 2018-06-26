using System;
using System.ComponentModel.DataAnnotations;
using AerariumTech.Pharmacy.Validations;

namespace AerariumTech.Pharmacy.Models.StocksViewModels
{
    public class CreateStockViewModel
    {
        [DataType(DataType.Date)]
        public DateTime DateOfFabrication { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime DateOfExpiration { get; set; }

        public long ProductId { get; set; }

        [Amount]
        public int Quantity { get; set; }
    }
}