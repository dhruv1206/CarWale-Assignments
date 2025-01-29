using System;
using System.ComponentModel.DataAnnotations;
using Stocks.Application.Entities;
using Stocks.Application.Extensions;

namespace Stocks.Application.Validations
{
    public class FuelTypeValidationAttribute : ValidationAttribute
    {
        private readonly string[] _allowedFuelTypes = EnumExtensions.GetDisplayNames<FuelType>();

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null && !_allowedFuelTypes.Contains(value.ToString()))
            {
                return new ValidationResult($"The Fuel Type must be one of the following: {string.Join(", ", _allowedFuelTypes)}.");
            }
            return ValidationResult.Success;
        }
    }
}