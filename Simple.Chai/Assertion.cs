// File: Simple/Chai/Assertion.cs
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Simple.Chai
{
    public static class Expect
    {
        public static Assertion<T> That<T>(T actual) => new Assertion<T>(actual);
    }

    public partial class Assertion<T>
    {
        private readonly T _actual;
        private readonly bool _negate;

        public Assertion(T actual, bool negate = false)
        {
            _actual = actual;
            _negate = negate;
        }

        public Assertion<T> To => this;
        public Assertion<T> Be => this;
        public Assertion<T> Have => this;
        public Assertion<T> Not => new Assertion<T>(_actual, !_negate);

        private void Fail(string message) => throw new ChaiException(message);

        public void Null()
        {
            bool result = object.Equals(_actual, null);
            if (_negate ? result : !result)
                Fail($"Expected value {(_negate ? "not " : "")}to be null.");
        }

        public void Ok()
        {
            bool result = _actual switch
            {
                null => false,
                bool b => b,
                string s => s.Length > 0,
                ICollection c => c.Count > 0,
                IEnumerable e => e.Cast<object>().Any(),
                _ => true
            };

            if (_negate ? result : !result)
                Fail($"Expected value {(_negate ? "not " : "")}to be truthy.");
        }


        public void A<U>()
        {
            bool result = _actual is U;
            if (_negate ? result : !result)
                Fail($"Expected value {(_negate ? "not " : "")}to be of type {typeof(U).Name}.");
        }

        public void InstanceOf<U>()
        {
            bool result = _actual?.GetType() == typeof(U);
            if (_negate ? result : !result)
                Fail($"Expected value {(_negate ? "not " : "")}to be instance of {typeof(U).Name}, but got {_actual?.GetType().Name}.");
        }

        public void Property(string name)
        {
            if (_actual == null)
                Fail("Expected object to have property, but value was null.");

            var type = _actual.GetType();
            var prop = type.GetProperty(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase);
            bool result = prop != null;

            if (_negate ? result : !result)
                Fail($"Expected object {(_negate ? "not " : "")}to have property \"{name}\".");
        }

        public void OwnProperty(string name)
        {
            if (_actual == null)
                Fail("Expected object to have own property, but value was null.");

            var type = _actual.GetType();
            var prop = type.GetProperty(name, BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase);
            bool result = prop != null;

            if (_negate ? result : !result)
                Fail($"Expected object {(_negate ? "not " : "")}to have own property \"{name}\".");
        }


        public void Satisfy(Func<T, bool> predicate)
        {
            bool result = predicate(_actual);
            if (_negate ? result : !result)
                Fail($"Expected value {(_negate ? "not " : "")}to satisfy predicate, but it did{(_negate ? "" : " not")}.");
        }
    }
}
