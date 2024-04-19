using FluentValidation;

namespace Xacosta.AdminPermissions.Application.Feature
{
    public class RequestPermissionsCommandValidator : AbstractValidator<RequestPermissionsCommand>
    {
        public RequestPermissionsCommandValidator()
        {
            RuleFor(cmd => cmd.ApellidoEmpleado)
                .NotEmpty().WithErrorCode("Field.Expected").WithMessage("{PropertyName} es requerido")
                .MaximumLength(50).WithErrorCode("Field.Error").WithMessage("{PropertyName} error MaximumLength");

            RuleFor(cmd => cmd.NombreEmpleado)
                .MaximumLength(50).WithErrorCode("Field.Error").WithMessage("{PropertyName} error MaximumLength");

            RuleFor(cmd => cmd.TipoPermiso)
                .NotEmpty().WithErrorCode("Field.Expected").WithMessage("{PropertyName} es requerido")
                .MaximumLength(50).WithErrorCode("Field.Error").WithMessage("{PropertyName} error MaximumLength");
        }
    }
}
