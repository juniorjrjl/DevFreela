using DevFreela.API.InputModel;
using FluentValidation;

namespace DevFreela.API.Validators;

public class NewUserInputModelValidator : AbstractValidator<NewUserInputModel>
{
    
    public NewUserInputModelValidator()
    {
        RuleFor(p => p.Email)
            .NotEmpty()
            .NotNull()
            .EmailAddress()
            .WithMessage("Informe um e-mail válido");


        RuleFor(p => p.Name)
            .NotEmpty()
            .NotNull()
            .WithMessage("Informe um nome válido");

        RuleFor(p => p.Name)
            .MaximumLength(255)
            .WithMessage("O nome deve ter no máximo 255 caractéres");

        RuleFor(p => p.BirthDate)
            .Must(BeGreatherThanEigthTeen)
            .WithMessage("O usuário deve ter mais de 18 anos");

        RuleFor(p => p.Password)
            .Equal(p => p.PasswordConfirm)
            .WithMessage("Os campos 'Password' e 'PasswordConfirm' devem ser iguais");

    }

    protected bool BeGreatherThanEigthTeen(DateTime birthDate) => (DateTime.Now - birthDate).TotalDays / 365 > 18;

}
