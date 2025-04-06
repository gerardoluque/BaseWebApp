using FluentValidation;
using API.Application.Grupos.Commands;

namespace API.Application.Grupos.Validators
{
    public class CreateGrupoValidator : AbstractValidator<CreateGrupo.Command>
    {
        public CreateGrupoValidator()
        {
            RuleFor(x => x.Descr)
                .NotEmpty().WithMessage("La descripción es obligatoria.")
                .MaximumLength(100).WithMessage("La descripción no puede tener más de 100 caracteres.");

            RuleFor(x => x.Descr)
                .Matches("^[a-zA-Z0-9 ]*$").WithMessage("La descripción solo puede contener letras, números y espacios.");
        }
    }
}