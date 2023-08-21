using MinhaAgenda.Domain.Dtos;
using MinhaAgenda.Domain.Entities;

namespace MinhaAgenda.Domain
{
    public class Pessoa
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public List<Contato> Contatos { get; set; }
        public int TotalContatos { get; set; }

        public static Pessoa MapPessoaDtoToPessoa(PessoaDto pessoaDto)
        {
            return new Pessoa
            {
                Nome = pessoaDto.Nome,
                Contatos = pessoaDto.Contatos
                .Select(c => new Contato
                {
                    Id = 0,
                    Numero = c.Numero,
                    TipoContato = (TipoContato)c.TipoContato
                }).ToList(),
                TotalContatos = pessoaDto.Contatos.Count()
            };
        }
    }
}