using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinhaAgenda.Infrastructure.Exceptions
{
    public class DomainConflitcException : Exception
    {
        public DomainConflitcException(string message) : base(message) { }
        public DomainConflitcException(string message, System.Exception innerException)
            : base(message, innerException) { }
    }
}
