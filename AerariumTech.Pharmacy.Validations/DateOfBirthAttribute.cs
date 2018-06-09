using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace AerariumTech.Pharmacy.Validations
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.Property)]
    public sealed class DateOfBirthAttribute : DataTypeAttribute
    {
        public DateOfBirthAttribute() : base(DataType.Date)
        {
            ErrorMessage = "The field {0} must be between {0} and {1} years old.";
        }

        public double MinimumAge { get; set; } = 0;
        public double MaximumAge { get; set; } = 100;

        public override string FormatErrorMessage(string name)
        {
            return string.Format(CultureInfo.CurrentCulture, ErrorMessageString, name, MinimumAge, MaximumAge);
        }

        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true;
            }

            return value is DateTime date && Validate(date);
        }

        private bool Validate(DateTime value)
        {
            var age = Math.Floor((DateTime.Now - value).TotalDays / 365);

            return age > MinimumAge && age < MaximumAge;
        }
    }
}