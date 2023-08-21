using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinhaAgenda.Domain.Dtos
{
    public class ContatoDto
    {
        public TipoContato TipoContato { get; set; }
        public string Numero { get; set; }
    }
}
