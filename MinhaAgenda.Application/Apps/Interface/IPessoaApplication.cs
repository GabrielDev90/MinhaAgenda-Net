using MinhaAgenda.Domain;
using MinhaAgenda.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinhaAgenda.Application.Apps.Interface
{
    public interface IPessoaApplication
    {
        public Task<List<Pessoa>> RetornarPessoas();
        public Task<Pessoa> RetornaPessoaPorNome(string Nome);
        public Task<Pessoa> InserirPessoa(PessoaDto pessoaDto);
        public Task<int> DeletarPessoa(int Id);
        public Task<Pessoa> AtualizarPessoa(string Nome, PessoaDto pessoaDto);
    }
}
