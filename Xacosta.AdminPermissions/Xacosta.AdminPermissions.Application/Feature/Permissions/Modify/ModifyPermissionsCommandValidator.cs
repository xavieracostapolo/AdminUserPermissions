using FluentValidation;

namespace Xacosta.AdminPermissions.Application.Feature
{
    public class ModifyPermissionsCommandValidator : AbstractValidator<ModifyPermissionsCommand>
    {
        public ModifyPermissionsCommandValidator()
        {
            RuleFor(cmd => cmd.ApellidoEmpleado)
                .NotEmpty().WithErrorCode("Field.Expected").WithMessage("{PropertyName} es requerido")
                .MaximumLength(50).WithErrorCode("Field.Error").WithMessage("{PropertyName} error MaximumLength");

            RuleFor(cmd => cmd.NombreEmpleado)
                .NotEmpty().WithErrorCode("Field.Expected").WithMessage("{PropertyName} es requerido")
                .MaximumLength(50).WithErrorCode("Field.Error").WithMessage("{PropertyName} error MaximumLength");

            RuleFor(cmd => cmd.TipoPermiso)
                .NotEmpty().WithErrorCode("Field.Expected").WithMessage("{PropertyName} es requerido");

            RuleFor(cmd => cmd.IdPermiso)
                .NotEmpty().WithErrorCode("Field.Expected").WithMessage("{PropertyName} es requerido");
        }
    }
}
