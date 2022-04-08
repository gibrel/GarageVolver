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
    }
}
