namespace AerariumTech.Pharmacy.Domain
{
    public class StateProvince
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Abbreviation { get; set; }
        
        public ShippingRate ShippingRate { get; set; }
    }
}