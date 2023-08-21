using MinhaAgenda.Application.Apps;
using MinhaAgenda.Domain;
using MinhaAgenda.Domain.Dtos;
using MinhaAgenda.Domain.Entities;
using MinhaAgenda.Infrastructure.Exceptions;
using MinhaAgenda.Infrastructure.Repositories.Interfaces;
using Moq;

namespace MinhaAgenda.Test
{
    public class PessoaApplicationUnitTest
    {
        private Mock<IPessoaRepository> mockIPessoaRepository;
        private PessoaApplication pessoaApplication;

        [SetUp]
        public void Setup()
        {
            mockIPessoaRepository = new Mock<IPessoaRepository>();
            pessoaApplication = new PessoaApplication(mockIPessoaRepository.Object);
        }

        [Test]
        public void VerificaSeDadosDaPessoaSaoValidos_DeveJogarException()
        {
            var exception = Assert.ThrowsAsync<FluentValidation.ValidationException>(async () =>
            {
                await pessoaApplication.InserirPessoa(new PessoaDto()
                {
                    Nome = "",
                    Contatos = new List<ContatoDto>()

                });
            });
            Assert.That(exception.Message.Contains("Nome"));
            Assert.That(exception.Message.Contains("Nome pessoa não poder ser vazio"));
            Assert.That(exception.Message.Contains("Contatos"));
            Assert.That(exception.Message.Contains("Contatos não pode ser vazio"));
        }

        [Test]
        public void VerificaSeNomeEVazioOuNullEVerificaMensagemDeNomeNaoPodeSerVazio_DeveJogarException()
        {
            var exception = Assert.ThrowsAsync<DomainException>(async () => { await pessoaApplication.RetornaPessoaPorNome(""); });
            Assert.That(exception.Message, Is.EqualTo("Nome não pode ser vazio"));
        }

        [Test]
        public async Task InserirClienteValidoNoBanco_DeveRetonarObjPessoaComIdAsync()
        {
            var pessoaDto = new PessoaDto()
            {
                Nome = "Gabriel",
                Contatos = new List<ContatoDto>()
                {
                    new ContatoDto()
                    {
                        Numero = "(31) 915479654", TipoContato = TipoContato.telefone
                    }
                }
            };

            var pessoa = new Pessoa()
            {
                Id = 1,
                Nome = "Gabriel",
                Contatos = new List<Contato>()
                {
                    new Contato()
                    {
                        Id = 1, Numero = "(31) 915479654", TipoContato = TipoContato.telefone
                    }
                },
                TotalContatos = 1
            };

            mockIPessoaRepository.Setup(x => x.RetornaPessoaPorNome("")).Returns<Pessoa>(null);

            mockIPessoaRepository
              .Setup(x => x.InserirPessoa(It.IsAny<Pessoa>()))
              .ReturnsAsync(pessoa);

            var retorno = await pessoaApplication.InserirPessoa(pessoaDto);

            Assert.IsNotNull(retorno);
        }
    }
}