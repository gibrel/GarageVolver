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
    }
}
