using MinhaAgenda.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinhaAgenda.Domain.Dtos
{
    public class PessoaDto
    {
        public string Nome { get; set; }
        public List<ContatoDto> Contatos { get; set; }
    }
}
