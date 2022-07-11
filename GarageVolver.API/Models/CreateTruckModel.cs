using GarageVolver.Domain.Entities;
using GarageVolver.Domain.Helpers;
using System.ComponentModel.DataAnnotations;

namespace GarageVolver.API.Models
{
    public class CreateTruckModel
    {
        [Required]
        [StringLength(2)]
        [RegularExpression("^(?:FM|FH)$", ErrorMessage = @"Model must be ""FM"" or ""FH""")]
        public string ModelName { get; set; }

        [Required]
        public int ManufacturingYear { get; set; }

        [Required]
        public int ModelYear { get; set; }

        public CreateTruckModel(string modelName, int manufacturingYear, int modelYear)
        {
            ModelName = modelName;
            ManufacturingYear = manufacturingYear;
            ModelYear = modelYear;
        }

        public static implicit operator CreateTruckModel(Truck truck)
        {
            return new(
                modelName: truck.Model.Name,
                manufacturingYear: truck.ManufacturingYear,
                modelYear: truck.ModelYear);
        }

        public static implicit operator Truck(CreateTruckModel createTruckModel)
        {
            return new (
                model: Enumeration.GetByName<TruckModel>(createTruckModel.ModelName),
                manufacturingYear: createTruckModel.ManufacturingYear,
                modelYear: createTruckModel.ModelYear);
        }
    }
}
