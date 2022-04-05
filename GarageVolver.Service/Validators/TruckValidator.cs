using FluentValidation;
using GarageVolver.Domain.Entities;

namespace GarageVolver.Service.Validators
{
    public class TruckValidator : AbstractValidator<Truck>
    {
        public TruckValidator()
        {
            RuleFor(t => t.Model)
                .NotEmpty().WithMessage("Please insert truck model.")
                .NotNull().WithMessage("Please insert truck model.");

            RuleFor(t => t.ModelYear)
                .NotEmpty().WithMessage("Please insert model year.")
                .NotNull().WithMessage("Please insert model year.");

            RuleFor(t => t.ManufacturingYear)
                .NotEmpty().WithMessage("Please insert manufacturing year.")
                .NotNull().WithMessage("Please insert manufacturing year.");
        }
    }
}
