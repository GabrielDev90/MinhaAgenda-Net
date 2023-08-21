using Microsoft.AspNetCore.Mvc;
using MinhaAgenda.Application.Apps.Interface;
using MinhaAgenda.Domain.Dtos;
using MinhaAgenda.Infrastructure.Exceptions;

namespace MinhaAgenda.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PessoaController : ControllerBase
    {
        private readonly IPessoaApplication pessoaApplication;

        public PessoaController(IPessoaApplication pessoaApplication)
        {
            this.pessoaApplication = pessoaApplication;
        }

        [HttpGet("RetornaPessoas")]
        public async Task<IActionResult> RetornaPessoas()
        {
            var result = await pessoaApplication.RetornarPessoas();
            return Ok(result);
        }

        [HttpGet("RetornaPessoaPorNome/{Nome}")]
        public async Task<IActionResult> RetornaPessoaPorNome(string Nome)
        {
            var result = await pessoaApplication.RetornaPessoaPorNome(Nome);
            return Ok(result);
        }

        [HttpPost("InserirPessoa")]
        public async Task<IActionResult> InserirPessoa(PessoaDto pessoaDto)
        {
            var result = await pessoaApplication.InserirPessoa(pessoaDto);
            return Ok(result);
        }

        [HttpDelete("DeletarPessoa/{Id}")]
        public async Task<IActionResult> DeletarPessoa(int Id)
        {
            await pessoaApplication.DeletarPessoa(Id);
            return NoContent();
        }

        [HttpPut("AtualizarPessoa/{Nome}")]
        public async Task<IActionResult> AtualizarPessoa(string Nome, PessoaDto pessoaDto)
        {
            var result = await pessoaApplication.AtualizarPessoa(Nome, pessoaDto);
            return Ok(result);
        }
    }
}
