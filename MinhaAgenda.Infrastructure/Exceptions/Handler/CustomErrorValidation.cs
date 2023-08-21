using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinhaAgenda.Infrastructure.Exceptions.Handler
{
    public class CustomErrorValidation
    {
        public string ErrorType { get; set; }
        public string ErrorMessage { get; set; }
        public List<Validation> Validations { get; set; }
    }
}
