using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.Chai
{
    public partial class Assertion<T>
    {
        public void CloseTo(double expected, double delta)
        {
            if (_actual is not IConvertible)
                throw new ChaiException("CloseTo() only supports numeric values.");

            double actualVal = Convert.ToDouble(_actual);
            bool result = Math.Abs(actualVal - expected) <= delta;

            if (_negate ? result : !result)
                Fail($"Expected value {(_negate ? "not " : "")}to be within {delta} of {expected}, but got {actualVal}.");
        }

        public void WithinPercent(double expected, double percent)
        {
            if (_actual is not IConvertible)
                throw new ChaiException("WithinPercent() only supports numeric values.");

            double actualVal = Convert.ToDouble(_actual);
            double delta = expected * percent / 100.0;
            bool result = Math.Abs(actualVal - expected) <= delta;

            if (_negate ? result : !result)
                Fail($"Expected value {(_negate ? "not " : "")}to be within {percent}% of {expected}, but got {actualVal}.");
        }
    }
}
