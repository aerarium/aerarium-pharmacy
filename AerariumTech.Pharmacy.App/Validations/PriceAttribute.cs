using System;
using System.ComponentModel.DataAnnotations;

namespace AerariumTech.Pharmacy.App.Validations
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.Property)]
    public class PriceAttribute : DataTypeAttribute
    {
        public PriceAttribute()
            : base(DataType.Currency)
        {
        }

        public decimal Minimum { get; set; }
        public decimal Maximum { get; set; } = 10000;

        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true;
            }

            return value is decimal price && Validate(price)
                   || value is double cost && Validate(cost);
        }

        private bool Validate(double value)
        {
            return value > (double) Minimum && value < (double) Maximum;
        }

        private bool Validate(decimal value)
        {
            return value > Minimum && value < Maximum;
        }
    }
}