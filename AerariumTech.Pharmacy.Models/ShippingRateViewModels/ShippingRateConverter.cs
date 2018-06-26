using AerariumTech.Pharmacy.Domain;

namespace AerariumTech.Pharmacy.Models.ShippingRateViewModels
{
    public class ShippingRateConverter
    {
        public static SelectShippingRateViewModel Convert(ShippingRate model)
            => new SelectShippingRateViewModel
            {
                Value = model.Id,
                Text = $"{model.StateProvince.Abbreviation} - {model.StateProvince.Name}"
            };
    }
}