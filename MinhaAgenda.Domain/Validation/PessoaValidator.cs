using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinhaAgenda.Domain.Validation
{
    public class PessoaValidator : AbstractValidator<Pessoa>
    {
        public PessoaValidator()
        {
            RuleFor(x => x.Nome).NotEmpty().WithMessage("Nome pessoa não poder ser vazio");
            RuleFor(x => x.Contatos).NotEmpty().WithMessage("Contatos não pode ser vazio");
        }
    }
}
