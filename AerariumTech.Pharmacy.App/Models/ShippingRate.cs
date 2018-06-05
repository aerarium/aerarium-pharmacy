namespace AerariumTech.Pharmacy.App.Models
{
    /// <summary>
    /// <see cref="ShippingRate"/> is an entity related to <see cref="StateProvince"/>s,
    /// sets the price each state must pay for its shipping.
    /// </summary>
    public class ShippingRate
    {
        public long Id { get; set; }
        public decimal Price { get; set; }
        public long StateProvinceId { get; set; }
        
        public StateProvince StateProvince { get; set; }
    }
}