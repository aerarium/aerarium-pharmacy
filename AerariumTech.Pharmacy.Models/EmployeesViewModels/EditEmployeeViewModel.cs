using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AerariumTech.Pharmacy.Validations;

namespace AerariumTech.Pharmacy.Models.EmployeesViewModels
{
    public class EditEmployeeViewModel
    {
        public long Id { get; set; }

        public string Name { get; set; }

        [CPF]
        public string Cpf { get; set; }

        public string Address { get; set; }

        [DataType(DataType.PostalCode)]
        public string PostalCode { get; set; }

        public string UserName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        public ICollection<string> Roles { get; set; }
    }
}