using AerariumTech.Pharmacy.Domain;

namespace AerariumTech.Pharmacy.Models.EmployeesViewModels
{
    public static class EmployeesConverter
    {
        public static User Convert(CreateEmployeeViewModel model)
            => new User
            {
                Name = model.Name,
                Cpf = model.Cpf,
                Address = model.Address,
                PostalCode = model.PostalCode,
                UserName = model.UserName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber
            };

        public static User Convert(EditEmployeeViewModel model)
            => new User
            {
                Id = model.Id,
                Name = model.Name,
                Cpf = model.Cpf,
                Address = model.Address,
                PostalCode = model.PostalCode,
                UserName = model.UserName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber
            };

        public static EditEmployeeViewModel Convert(User model)
            => new EditEmployeeViewModel
            {
                Id = model.Id,
                Name = model.Name,
                Cpf = model.Cpf,
                Address = model.Address,
                PostalCode = model.PostalCode,
                UserName = model.UserName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber
            };
    }
}