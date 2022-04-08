using System.ComponentModel.DataAnnotations;

namespace GarageVolver.API.Models
{
    public class GetTruckModel : CreateTruckModel
    {
        [Key]
        [Required]
        public int Id { get; set; }
    }
}
