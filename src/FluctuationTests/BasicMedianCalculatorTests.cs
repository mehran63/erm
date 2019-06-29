using Fluctuation;
using FluentAssertions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace FluctuationTests
{
    public class BasicMedianCalculatorTests
    {
        [Theory]
        [ClassData(typeof(BasicMedianCalculatorData))]
        public void RoundTest(decimal[] values, decimal expectedMedian)
        {
            //arrange
            var sut = new BasicMedianCalculator();

            //act
            var actualResult = sut.Calculate(values.ToList());

            //assert
            actualResult.Should().Be(expectedMedian);

        }

        public class BasicMedianCalculatorData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { new[] { 10M }, 10M };
                yield return new object[] { new[] { 1M, 4M }, 2.5M };
                yield return new object[] { new[] { 1M, 5M, 7M }, 5M };
                yield return new object[] { new[] { 1M, 5M, 7M, 9M }, 6M };
                yield return new object[] { new[] { 1M, 5M, 7M, 9M, 12M }, 7M };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
