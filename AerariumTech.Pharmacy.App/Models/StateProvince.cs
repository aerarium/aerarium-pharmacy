using System.Collections.Generic;

namespace AerariumTech.Pharmacy.App.Models
{
    public class StateProvince
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Abbreviation { get; set; }
        
        public ShippingRate ShippingRate { get; set; }
    }
}