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
        public void True()
        {
            bool result = _actual is bool b && b;
            if (_negate ? result : !result)
                Fail($"Expected value {(_negate ? "not " : "")}to be true.");
        }

        public void False()
        {
            bool result = _actual is bool b && !b;
            if (_negate ? result : !result)
                Fail($"Expected value {(_negate ? "not " : "")}to be false.");
        }

        public void Falsy()
        {
            bool result = _actual switch
            {
                null => true,
                bool b => !b,
                string s => s.Length == 0,
                ICollection c => c.Count == 0,
                IEnumerable e => !e.Cast<object>().Any(),
                _ => false
            };

            if (_negate ? result : !result)
                Fail($"Expected value {(_negate ? "not " : "")}to be falsy.");
        }
    }
}
