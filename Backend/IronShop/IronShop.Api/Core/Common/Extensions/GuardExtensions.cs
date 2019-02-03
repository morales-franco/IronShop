using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ardalis.GuardClauses
{
    public static class IronGuards
    {
        public static void UpperZero(this IGuardClause guardClause, decimal value)
        {
            if (value <= 0)
                throw new ValidationException($"The value { value} must be upper zero");
        }
    }
}