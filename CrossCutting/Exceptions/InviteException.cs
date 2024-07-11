using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossCutting.Exceptions
{
    public class InviteException : Exception
    {
        public InviteException(string message) : base(message)
        {

        }
    }
}
