using MinhaAgenda.Domain;
using MinhaAgenda.Domain.Dtos;
using MinhaAgenda.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinhaAgenda.Infrastructure.Repositories.Interfaces
{
    public interface IPessoaRepository
    {
        public Task<Pessoa> InserirPessoa(Pessoa pessoa);
        public Task<List<Pessoa>> RetornaPessoas();
        public Task<Pessoa> RetornaPessoaPorNome(string Nome);
        public Task<Pessoa> RetornaPessoaPorId(int Id);
        public Task<int> DeletaPessoa(Pessoa pessoa);
        public Task<Pessoa> AtualizaPessoa(Pessoa pessoa);
    }
}
