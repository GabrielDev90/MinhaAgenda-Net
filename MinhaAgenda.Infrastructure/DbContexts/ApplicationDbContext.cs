using Microsoft.EntityFrameworkCore;
using MinhaAgenda.Domain;
using MinhaAgenda.Domain.Entities;
using System.Reflection.Metadata;

namespace MinhaAgenda.Infrastructure.DbContexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Pessoa> Pessoas { get; set; }

        public DbSet<Contato> Contatos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pessoa>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<Pessoa>()
                .HasMany(c => c.Contatos)
                .WithOne(p => p.Pessoa)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<Contato>()
              .HasKey(c => c.Id);

            modelBuilder.Entity<Contato>()
              .HasOne(p => p.Pessoa)
              .WithMany(c => c.Contatos);
        }
    }
}