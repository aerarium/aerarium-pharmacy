using System;
using System.ComponentModel.DataAnnotations;

namespace AerariumTech.Pharmacy.Validations
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.Property)]
    public class AmountAttribute : RangeAttribute
    {
        public AmountAttribute() : base(1, int.MaxValue)
        {
        }
    }
}
