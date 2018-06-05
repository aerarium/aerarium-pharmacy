-using System;
using System.ComponentModel.DataAnnotations;
using AerariumTech.Pharmacy.App.Validations;

namespace AerariumTech.Pharmacy.App.Models
{
    public abstract class Person
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "Name is required.", AllowEmptyStrings = false)]
        [StringLength(50, ErrorMessage = "Name can only contain {0} characters.")]
        public string Name { get; set; }

        [CPF(ErrorMessage = "Invalid CPF.")]
        [StringLength(14, ErrorMessage = "CPF can only contain {0} characters.", MinimumLength = 14)]
        public string Cpf { get; set; }

        [StringLength(200, ErrorMessage = "Address can only contain until {1} characters.")]
        [Required(ErrorMessage = "Address is required.", AllowEmptyStrings = false)]
        public string Address { get; set; }

        [Required]
        [StringLength(9, ErrorMessage = "Post code must contain {0} characters.", MinimumLength = 9)]
        public string PostCode { get; set; }

        [Phone] [StringLength(50)] public string Phone { get; set; }

        [EmailAddress] [StringLength(50)] public string Email { get; set; }

        [DateOfBirth]
        public DateTime DateOfBirth { get; set; }
    }
}