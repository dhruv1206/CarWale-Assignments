using System;
using System.ComponentModel.DataAnnotations;

namespace Stocks.Application.Common
{
    public class IndividualFilter
    {
        public string? PropertyName { get; set; }
        public object? Value { get; set; }
        public Comparison Comparison { get; set; }
    }


    public enum Comparison
    {
        [Display(Name = "==")]
        Equal,

        [Display(Name = "<")]
        LessThan,

        [Display(Name = "<=")]
        LessThanOrEqual,

        [Display(Name = ">")]
        GreaterThan,

        [Display(Name = ">=")]
        GreaterThanOrEqual,

        [Display(Name = "!=")]
        NotEqual,

        [Display(Name = "In")]
        In,

    }
}
