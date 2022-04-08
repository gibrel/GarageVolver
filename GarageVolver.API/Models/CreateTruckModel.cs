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
        [RegularExpression(@"^(2022)$", ErrorMessage = @$"Manufacturing year must be current year.")]
        public int ManufacturingYear { get; set; }

        [Required]
        [RegularExpression(@"^(202[2-3])$", ErrorMessage = @$"Model year must be current or next year.")]
        public int ModelYear { get; set; }

        [Required]
        [StringLength(7)]
        [RegularExpression(@"^([A-Z]{3}[0-9]{1}[A-Z]{1}[0-9]{2}\b)$", ErrorMessage = @"Licence Plate must like ""ABC1D23""")]
        public string LicencePlate { get; set; }
    }
}
