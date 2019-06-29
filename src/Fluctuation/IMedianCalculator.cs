using System.Collections.Generic;

namespace Fluctuation
{
    public interface IMedianCalculator
    {
        decimal Calculate(List<decimal> values);
    }
}