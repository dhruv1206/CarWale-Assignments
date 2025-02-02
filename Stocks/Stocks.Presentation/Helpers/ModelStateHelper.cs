﻿using System.Linq;
using System.Collections.Generic;
using System;

using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Stocks.Presentation.Helpers
{
    public static class ModelStateHelper
    {
        public static string GetErrors(ModelStateDictionary modelState)
        {
            var errors = modelState
                .Where(e => e.Value.Errors.Any())
                .ToDictionary(
                    kvp => kvp.Key,
                    kvp => string.Join(", ", kvp.Value.Errors.Select(error => error.ErrorMessage))
                );

            return string.Join(", ", errors.Select(kvp => $"{kvp.Key}: {kvp.Value}"));
        }
    }

}
