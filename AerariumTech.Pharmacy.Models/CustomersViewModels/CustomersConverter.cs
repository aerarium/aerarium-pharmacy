using AerariumTech.Pharmacy.Domain;

namespace AerariumTech.Pharmacy.Models.CustomersViewModels
{
    public static class CustomersConverter
    {
        public static User Convert(CreateCustomerViewModel model)
            => new User
            {
                UserName = model.UserName,
                Cpf = model.Cpf,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                Address = model.Address,
                PostalCode = model.PostalCode
            };

        public static User Convert(EditCustomerViewModel model)
            => new User
            {
                Id = model.Id,
                UserName = model.UserName,
                Cpf = model.Cpf,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                Address = model.Address,
                PostalCode = model.PostalCode
            };

        public static EditCustomerViewModel Convert(User model)
            => new EditCustomerViewModel
            {
                Id = model.Id,
                UserName = model.UserName,
                Cpf = model.Cpf,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                Address = model.Address,
                PostalCode = model.PostalCode
            };
    }
}