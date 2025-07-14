using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.Chai
{
    public class ChaiException : Exception
    {
        public ChaiException(string message) : base(message) { }
    }
}
