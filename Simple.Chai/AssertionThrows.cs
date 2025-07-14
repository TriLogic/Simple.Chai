using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Simple.Chai
{
    public partial class Assertion<T>
    {
        public void Throws<TException>() where TException : Exception
        {
            if (_actual is not Delegate del)
                throw new ChaiException("Throws<T>() must be called on a delegate (Action or Func).");

            bool threwExpected = false;
            try
            {
                del.DynamicInvoke();
            }
            catch (TargetInvocationException ex) when (ex.InnerException is TException)
            {
                threwExpected = true;
            }
            catch
            {
                // wrong exception type
            }

            if (_negate ? threwExpected : !threwExpected)
                Fail($"Expected delegate {(_negate ? "not " : "")}to throw {typeof(TException).Name}.");
        }

        public void Throws<TException>(string messagePart) where TException : Exception
        {
            if (_actual is not Delegate del)
                throw new ChaiException("Throws<T>(string) must be called on a delegate (Action or Func).");

            bool match = false;
            try
            {
                del.DynamicInvoke();
            }
            catch (TargetInvocationException ex) when (ex.InnerException is TException tex)
            {
                match = tex.Message != null && tex.Message.Contains(messagePart);
            }
            catch
            {
                // wrong exception type
            }

            if (_negate ? match : !match)
                Fail($"Expected {typeof(TException).Name} message {(_negate ? "not " : "")}to contain \"{messagePart}\".");
        }
    }
}
