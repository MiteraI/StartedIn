using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossCutting.Exceptions
{
    public class TeamLimitException : Exception
    {
        public TeamLimitException(string message) : base(message)
        {
        }
    }
}
