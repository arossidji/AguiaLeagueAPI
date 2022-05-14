using FluentValidation;


namespace AguiaLeague.Domain.Validations.User;


public class AddUserValidation : AbstractValidator<Models.User>
{
    public AddUserValidation()
    {
        RuleFor(x => x.nome)
           .NotNull().WithMessage("O User precisa ter um nome.")
           .MinimumLength(3).WithMessage("O nome do User precisa ter pelo menos 3 caracteres.")
           .MaximumLength(15).WithMessage("O nome do User precisa ter menos de 16 caracteres.");

        RuleFor(x => x.email)
            .NotNull().WithMessage("O User precisa ter um email.")
            .MinimumLength(3).WithMessage("A email do User precisa ter pelo menos 3 caracteres.")
            .MaximumLength(5).WithMessage("A email do User precisa ter menos de 6 caracteres.");
    }
}

