using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using Stocks.Application.Entities;

namespace Stocks.Application.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDisplayName(this Enum enumValue)
        {
            return enumValue.GetType()
                            .GetMember(enumValue.ToString())
                            .FirstOrDefault()?
                            .GetCustomAttribute<DisplayAttribute>()?
                            .Name ?? enumValue.ToString();
        }
        public static string ToString(this Enum enumValue)
        {
            return enumValue.GetType()
                            .GetMember(enumValue.ToString())
                            .FirstOrDefault()?
                            .GetCustomAttribute<DisplayAttribute>()?
                            .Name ?? enumValue.ToString();
        }

        public static string[] GetDisplayNames<T>() where T : Enum
        {
            return Enum.GetValues(typeof(T))
                       .Cast<T>()
                       .Select(e => e.GetDisplayName())
                       .ToArray();
        }

        public static FuelType? FromString(string value)
        {
            return Enum.TryParse<FuelType>(value, true, out var result) ? result : (FuelType?)null;
        }

    }
}