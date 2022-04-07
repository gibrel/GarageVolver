using GarageVolver.Domain.Helpers;

namespace GarageVolver.Domain.Entities
{
    public class Truck : BaseEntity
    {
        public TruckModel Model { get; set; }
        public int ManufacturingYear { get; set; } = DateTime.Now.Year;
        public int ModelYear { get; set; }
    }

    public class TruckModel : Enumeration
    {
        public static readonly TruckModel FH = new(0, nameof(FH));
        public static readonly TruckModel FM = new(1, nameof(FM));
        //public static readonly TruckModel FMX = new(2, nameof(FMX));

        public TruckModel(int id, string name) : base(id, name)
        {
        }
    }
}
