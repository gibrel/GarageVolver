using GarageVolver.Domain.Entities;
using GarageVolver.Domain.Helpers;
using System.ComponentModel.DataAnnotations;

namespace GarageVolver.API.Models
{
    public class UpdateTruckModel
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(2)]
        [RegularExpression("^(?:FM|FH)$", ErrorMessage = @"Model must be ""FM"" or ""FH""")]
        public string ModelName { get; set; }

        [Required]
        public int ManufacturingYear { get; set; }

        [Required]
        public int ModelYear { get; set; }

        public UpdateTruckModel(int id, string modelName, int manufacturingYear, int modelYear)
        {
            Id = id;
            ModelName = modelName;
            ManufacturingYear = manufacturingYear;
            ModelYear = modelYear;
        }

        public static implicit operator UpdateTruckModel(Truck truck)
        {
            return new(
                id: truck.Id,
                modelName: truck.Model.Name,
                manufacturingYear: truck.ManufacturingYear,
                modelYear: truck.ModelYear);
        }

        public static implicit operator Truck(UpdateTruckModel updateTruckModel)
        {
            return new(
                model: Enumeration.GetByName<TruckModel>(updateTruckModel.ModelName),
                manufacturingYear: updateTruckModel.ManufacturingYear,
                modelYear: updateTruckModel.ModelYear);
        }
    }
}
