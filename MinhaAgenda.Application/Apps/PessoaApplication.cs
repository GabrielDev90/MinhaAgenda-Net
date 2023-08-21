using FluentValidation.Results;
using MinhaAgenda.Application.Apps.Interface;
using MinhaAgenda.Domain;
using MinhaAgenda.Domain.Dtos;
using MinhaAgenda.Domain.Validation;
using MinhaAgenda.Infrastructure.Exceptions;
using MinhaAgenda.Infrastructure.Exceptions.Handler;
using MinhaAgenda.Infrastructure.Repositories.Interfaces;
using Newtonsoft.Json;

namespace MinhaAgenda.Application.Apps
{
    public class PessoaApplication : IPessoaApplication
    {
        private readonly IPessoaRepository pessoaRepository;

        public PessoaApplication(IPessoaRepository pessoaRepository)
        {
            this.pessoaRepository = pessoaRepository;
        }

        public async Task<Pessoa> AtualizarPessoa(string nome, PessoaDto pessoaDto)
        {
            var pessoaNova = Pessoa.MapPessoaDtoToPessoa(pessoaDto);
            ValidaPessoa(pessoaNova);

            var pessoaNovaExiste = await pessoaRepository.RetornaPessoaPorNome(pessoaDto.Nome);

            if (pessoaNovaExiste is not null && nome != pessoaDto.Nome)
            {
                throwDomainException("Já existe um contato com nome informado!");
            }

            var pessoaAntiga = await RetornaPessoaPorNome(nome);

            pessoaAntiga.Nome = pessoaNova.Nome;
            pessoaAntiga.Contatos = pessoaNova.Contatos;
            pessoaAntiga.TotalContatos = pessoaAntiga.Contatos.Count();

            var result = await pessoaRepository.AtualizaPessoa(pessoaAntiga);

            return result;
        }

        public async Task<int> DeletarPessoa(int Id)
        {
            var pessoa = await pessoaRepository.RetornaPessoaPorId(Id);

            verificaPessoaExiste(pessoa);

            return await pessoaRepository.DeletaPessoa(pessoa);
        }

        public async Task<Pessoa> InserirPessoa(PessoaDto pessoaDto)
        {
            Pessoa retornoPessoa = null;
            var pessoa = Pessoa.MapPessoaDtoToPessoa(pessoaDto);
            ValidaPessoa(pessoa);

            var result = await pessoaRepository.RetornaPessoaPorNome(pessoa.Nome);

            if (result is not null)
            {
                throwDomainConflitcException("Pessoa já existe na lista de contatos");
            }

            retornoPessoa = await pessoaRepository.InserirPessoa(pessoa);

            return retornoPessoa;
        }

        public async Task<Pessoa> RetornaPessoaPorNome(string nome)
        {
            if (string.IsNullOrEmpty(nome))
            {
                throwDomainException("Nome não pode ser vazio");
            }

            var result = await pessoaRepository.RetornaPessoaPorNome(nome);

            verificaPessoaExiste(result);

            return result;
        }

        public async Task<List<Pessoa>> RetornarPessoas()
        {
            return await pessoaRepository.RetornaPessoas();
        }

        public void throwValidationException(List<ValidationFailure> errorList)
        {
            var teste = (from validation in errorList
                         select new Validation
                         {
                             PropertyName = validation.PropertyName,
                             ErrorMessage = validation.ErrorMessage
                         }).ToList();
            var resulta = JsonConvert.SerializeObject(teste);
            throw new FluentValidation.ValidationException(resulta);
        }

        public void ValidaPessoa(Pessoa pessoa)
        {
            PessoaValidator pessoaValidator = new PessoaValidator();
            var validacaoResult = pessoaValidator.Validate(pessoa);

            if (validacaoResult.Errors.Count != 0)
            {
                throwValidationException(validacaoResult.Errors);
            }
        }

        public void throwDomainException(string validacao)
        {
            throw new DomainException(validacao);
        }

        public void throwDomainConflitcException(string validacao)
        {
            throw new DomainConflitcException(validacao);
        }

        public void throwDomainNotFoundException(string validacao)
        {
            throw new DomainNotFoundException(validacao);
        }

        public void verificaPessoaExiste(Pessoa pessoa)
        {
            if (pessoa is null)
            {
                throwDomainNotFoundException("Pessoa não foi encontrada");
            }
        }
    }
}