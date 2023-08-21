using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MinhaAgenda.Domain.Entities
{
    public class Contato
    {
        public long Id { get; set; }
        public TipoContato TipoContato { get; set; }
        public string Numero { get; set; }

        [JsonIgnore]
        public Pessoa Pessoa { get; set; }
    }
}
