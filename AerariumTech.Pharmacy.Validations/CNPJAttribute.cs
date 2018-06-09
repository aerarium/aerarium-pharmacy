using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace AerariumTech.Pharmacy.Validations
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.Property)]
    public class CNPJAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true;
            }

            return value is string cnpj && Validate(cnpj);
        }

        private static bool Validate(string value)
        {
            var multiplicators = new[] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            value = value
                .Trim()
                .Replace(".", string.Empty)
                .Replace("-", string.Empty)
                .Replace("/", string.Empty);

            if (value.Length != 14)
            {
                return false;
            }

            var temp = value.Substring(0, 12);
            var sum = multiplicators //.Where((t, i) => i > 0) // Ignore first
                .Select((t, i) => i > 0 ? int.Parse(temp[i - 1].ToString()) * t : 0).Sum();

            var rest = sum % 11;

            if (rest < 2)
            {
                rest = 0;
            }
            else
            {
                rest = 11 - rest;
            }

            var digit = rest.ToString();
            temp += digit;
            sum = multiplicators.Select((t, i) => int.Parse(temp[i].ToString()) * t).Sum();

            rest = sum % 11;

            if (rest < 2)
            {
                rest = 0;
            }
            else
            {
                rest = 11 - rest;
            }

            digit += rest;
            return value.EndsWith(digit);
        }
    }
}