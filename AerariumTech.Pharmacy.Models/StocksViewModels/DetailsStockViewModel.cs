using System;
using AerariumTech.Pharmacy.Domain;

namespace AerariumTech.Pharmacy.Models.StocksViewModels
{
    public class DetailsStockViewModel
    {
        public long Id { get; set; }

        public DateTime DateOfFabrication { get; set; }

        public DateTime DateOfExpiration { get; set; }

        public Product Product { get; set; }

        /// <summary>
        /// How many units were there when it was registered
        /// </summary>
        public int Entrance { get; set; }
        
        /// <summary>
        /// Actual count of units
        /// </summary>
        public int Amount { get; set; }
    }
}
