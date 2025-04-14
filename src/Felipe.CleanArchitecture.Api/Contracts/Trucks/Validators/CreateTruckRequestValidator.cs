using FluentValidation;

namespace Felipe.CleanArchitecture.Api.Contracts.Trucks.Validators;

public class CreateTruckRequestValidator : AbstractValidator<CreateTruckRequest>
{
    public CreateTruckRequestValidator()
    {
        RuleFor(x => x.LicensePlate)
            .NotEmpty().WithMessage("A placa é obrigatória.")
            .MaximumLength(50).WithMessage("A placa deve ter no máximo 50 caracteres.");

        RuleFor(x => x.Model)
            .NotEmpty().WithMessage("O modelo é obrigatório.")
            .MaximumLength(50).WithMessage("O modelo deve ter no máximo 50 caracteres.");

        RuleFor(x => x.LastMaintenanceDate)
            .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("A data de manutenção não pode estar no futuro.")
            .When(x => x.LastMaintenanceDate.HasValue);

        RuleFor(x => x.ConfirmTerms)
            .Equal(true).WithMessage("Você deve aceitar os termos antes de continuar.");
    }
}
