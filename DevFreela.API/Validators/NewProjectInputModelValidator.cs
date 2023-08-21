using DevFreela.API.InputModel;
using FluentValidation;

namespace DevFreela.API.Validators
{
    public class NewProjectInputModelValidator : AbstractValidator<NewProjectInputModel>
    {
        
        public NewProjectInputModelValidator()
        {
            RuleFor(p => p.Description)
                .MaximumLength(255)
                .WithMessage("Informe uma descrição que tenha no máximo 255 caractéres");

            RuleFor(p => p.Description)
                .NotEmpty()
                .NotNull()
                .WithMessage("Informe a descrição do projeto");

            RuleFor(p => p.Title)
                .MaximumLength(30)
                .WithMessage("Informe um título que tenha no máximo 30 caractéres");

            RuleFor(p => p.Title)
                .NotEmpty()
                .NotNull()
                .WithMessage("Informe o título do projeto");

            RuleFor(p => p.ClientId)
                .GreaterThan(0)
                .WithMessage("Informe um id de cliente válido");

            RuleFor(p => p.FreelancerId)
                .GreaterThan(0)
                .WithMessage("Informe um id de freelancer válido");

            RuleFor(p => p.TotalCost)
                .GreaterThan(0)
                .WithMessage("O valor do projeto deve ser maior que zero");
        }

    }
}
