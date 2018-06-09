using AerariumTech.Pharmacy.Domain;
using AerariumTech.Pharmacy.Validations;
using System.Collections.Generic;

namespace AerariumTech.Pharmacy.Models.SalesViewModels
{
    public class CreateSaleViewModel
    {
        public class ItemSale
        {
            public string SerialCode { get; set; }
            public long ProductId { get; set; }
            public int Amount { get; set; }
        }
        
        [CPF]
        public string Cpf { get; set; }
        
        public List<ItemSale> ItemSales { get; set; }
        
        public User User { get; set; }
    }
}