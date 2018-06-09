using AerariumTech.Pharmacy.Domain;

namespace AerariumTech.Pharmacy.Models.AccountViewModels
{
    public static class AccountConverter
    {
        public static User Convert(RegisterViewModel model)
        {
            return new User
            {
                UserName = model.Name,
                Email = model.Email
            };
        }
    }
}