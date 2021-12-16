using FluentValidation;

namespace AguiaLeague.Domain.Validations.Time;

public class AddTimeValidation : AbstractValidator<Models.Time>
{
    public AddTimeValidation()
    {
        RuleFor(x => x.Nome)
            .NotNull().WithMessage("O time precisa ter um nome.")
            .MinimumLength(3).WithMessage("O nome do time precisa ter pelo menos 3 caracteres.")
            .MaximumLength(15).WithMessage("O nome do time precisa ter menos de 16 caracteres.");

        RuleFor(x => x.Tag)
            .NotNull().WithMessage("O time precisa ter um tag.")
            .MinimumLength(3).WithMessage("A tag do time precisa ter pelo menos 3 caracteres.")
            .MaximumLength(5).WithMessage("A tag do time precisa ter menos de 6 caracteres.");
    }
}