using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Simple.Chai
{
    public partial class Assertion<T>
    {
        public void LengthOf(int expectedLength)
        {
            int actualLength = _actual switch
            {
                string s => s.Length,
                ICollection c => c.Count,
                _ => throw new ChaiException("LengthOf() only supports strings or collections.")
            };

            if (_negate ? actualLength == expectedLength : actualLength != expectedLength)
                Fail($"Expected length {(_negate ? "not " : "")}to be {expectedLength}, but got {actualLength}.");
        }

        public void Empty()
        {
            bool result = _actual switch
            {
                string s => s.Length == 0,
                ICollection c => c.Count == 0,
                _ => throw new ChaiException("Empty() only supports strings or collections.")
            };

            if (_negate ? result : !result)
                Fail($"Expected value {(_negate ? "not " : "")}to be empty.");
        }

        public void Include(object item)
        {
            bool result = _actual switch
            {
                string s => s.Contains(item?.ToString()),
                IEnumerable e => e.Cast<object>().Contains(item),
                _ => throw new ChaiException("Include() only supports strings or collections.")
            };

            if (_negate ? result : !result)
                Fail($"Expected value {(_negate ? "not " : "")}to include {item}.");
        }

        public void Contain(string substring)
        {
            if (_actual is not string s)
                throw new ChaiException("Contain() only supports strings.");

            bool result = s.Contains(substring);
            if (_negate ? result : !result)
                Fail($"Expected string {(_negate ? "not " : "")}to contain \"{substring}\".");
        }

        public void StartWith(string prefix)
        {
            if (_actual is not string s)
                throw new ChaiException("StartWith() only supports strings.");

            bool result = s.StartsWith(prefix);
            if (_negate ? result : !result)
                Fail($"Expected string {(_negate ? "not " : "")}to start with \"{prefix}\".");
        }

        public void EndWith(string suffix)
        {
            if (_actual is not string s)
                throw new ChaiException("EndWith() only supports strings.");

            bool result = s.EndsWith(suffix);
            if (_negate ? result : !result)
                Fail($"Expected string {(_negate ? "not " : "")}to end with \"{suffix}\".");
        }

        public void Match(string pattern)
        {
            if (_actual is not string s)
                throw new ChaiException("Match() only supports strings.");

            bool result = Regex.IsMatch(s, pattern);
            if (_negate ? result : !result)
                Fail($"Expected string {(_negate ? "not " : "")}to match regex \"{pattern}\".");
        }


        public void Keys(params string[] expectedKeys)
        {
            if (_actual is not IDictionary dict)
                throw new ChaiException("Keys() only supports dictionaries.");

            foreach (var key in expectedKeys)
            {
                bool contains = dict.Contains(key);
                if (_negate ? contains : !contains)
                    Fail($"Expected dictionary {(_negate ? "not " : "")}to contain key \"{key}\".");
            }
        }

        public void Members(IEnumerable expected)
        {
            if (_actual is not IEnumerable actualEnum)
                throw new ChaiException("Members() only supports collections.");

            var actualList = actualEnum.Cast<object>().ToList();
            var expectedList = expected.Cast<object>().ToList();

            foreach (var item in expectedList)
            {
                bool contains = actualList.Contains(item);
                if (_negate ? contains : !contains)
                    Fail($"Expected collection {(_negate ? "not " : "")}to contain member {item}.");
            }
        }

    }
}
