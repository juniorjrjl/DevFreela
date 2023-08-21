using DevFreela.API.InputModel;
using FluentValidation;

namespace DevFreela.API.Validators
{
    public class NewCommentInputModelValidator : AbstractValidator<CreateCommentInputModel>
    {
        
        public NewCommentInputModelValidator()
        {
            RuleFor(p => p.Comment)
                .NotEmpty()
                .NotNull()
                .WithMessage("Informe o conteúdo do comentário");

            RuleFor(p => p.UserId)
                .GreaterThan(0)
                .WithMessage("Informe um id de cliente válido");

        }

    }
}
