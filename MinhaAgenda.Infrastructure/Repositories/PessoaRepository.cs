using Microsoft.EntityFrameworkCore;
using MinhaAgenda.Domain;
using MinhaAgenda.Domain.Entities;
using MinhaAgenda.Infrastructure.DbContexts;
using MinhaAgenda.Infrastructure.Repositories.Interfaces;

namespace MinhaAgenda.Infrastructure.Repositories
{
    public class PessoaRepository : IPessoaRepository
    {
        private readonly ApplicationDbContext applicationDbContext;

        public PessoaRepository(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public async Task<Pessoa> InserirPessoa(Pessoa pessoa)
        {
            applicationDbContext.Pessoas.Add(pessoa);
            await applicationDbContext.SaveChangesAsync();
            return pessoa;
        }

        public async Task<List<Pessoa>> RetornaPessoas()
        {
            return await applicationDbContext.Pessoas.Include(x => x.Contatos).AsNoTracking().ToListAsync();
        }

        public async Task<int> DeletaPessoa(Pessoa pessoa)
        {
            applicationDbContext.Pessoas.Attach(pessoa);
            applicationDbContext.Pessoas.Remove(pessoa);
            return await applicationDbContext.SaveChangesAsync();
        }

        public async Task<Pessoa> AtualizaPessoa(Pessoa pessoa)
        {
            applicationDbContext.Attach(pessoa);
            applicationDbContext.Update(pessoa);
            await applicationDbContext.SaveChangesAsync();
            return pessoa;
        }

        public async Task<Pessoa> RetornaPessoaPorNome(string Nome)
        {
            return await GetPessoas()
                .Include(x => x.Contatos)
                .Where(p => p.Nome.ToLower().Equals(Nome.ToLower()))
                .FirstOrDefaultAsync();
        }

        public async Task<Pessoa> RetornaPessoaPorId(int id)
        {
            return await GetPessoas()
                .Include(x => x.Contatos)
                .Where(p => p.Id.Equals(id))
                .FirstOrDefaultAsync();
        }

        private IQueryable<Pessoa> GetPessoas()
        {
            return applicationDbContext.Pessoas.AsQueryable();
        }
    }
}
