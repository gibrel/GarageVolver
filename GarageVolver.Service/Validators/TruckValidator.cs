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
                .NotNull().WithMessage("Please insert model year.")
                .GreaterThanOrEqualTo(DateTime.Now.Year)
                    .WithMessage(t => $"[ModelYear:{t.ModelYear}] Model year must be current year of next year.")
                .LessThanOrEqualTo(DateTime.Now.Year + 1)
                    .WithMessage(t => $"[ModelYear:{t.ModelYear}] Model year must be current year of next year.");

            RuleFor(t => t.ManufacturingYear)
                .NotEmpty().WithMessage("Please insert manufacturing year.")
                .NotNull().WithMessage("Please insert manufacturing year.")
                .Equal(DateTime.Now.Year)
                    .WithMessage(t => $"[ManufacturingYear:{t.ManufacturingYear}] Manufacturing year must be current year.");
        }
    }
}
