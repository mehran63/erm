using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fluctuation
{
    public class BasicMedianCalculator : IMedianCalculator
    {
        public decimal Calculate(List<decimal> values)
        {
            if (values == null || values.Count() == 0)
                throw new ArgumentException(nameof(values), "Null or empty list!");

            values.Sort();

            if (values.Count % 2 == 0)
            {
                var medianIndex = values.Count / 2;
                return (values[medianIndex - 1] +
                    values[medianIndex]) / 2;
            }
            else
            {
                return values[values.Count / 2];
            }

        }
    }
}
