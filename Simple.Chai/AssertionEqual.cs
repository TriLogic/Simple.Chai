using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.Chai
{
    public partial class Assertion<T>
    {
        public void Equal(object expected)
        {
            bool result = Equals(_actual, expected);
            if (_negate ? result : !result)
                Fail($"Expected value {(_negate ? "not " : "")}to equal {expected}, but got {_actual}.");
        }
    }
}
