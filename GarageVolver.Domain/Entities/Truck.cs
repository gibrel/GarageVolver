using GarageVolver.Domain.Helpers;

namespace GarageVolver.Domain.Entities
{
    public class Truck : BaseEntity
    {
        public TruckModel Model { get; set; }
        public int ManufacturingYear { get; set; }
        public int ModelYear { get; set; }

        public Truck(TruckModel model, int manufacturingYear, int modelYear = 0)
        {
            Model = model;
            ManufacturingYear = manufacturingYear;
            ModelYear = modelYear == 0 ? DateTime.Now.Year : modelYear;                
        }
    }

    public class TruckModel : Enumeration
    {
        public static readonly TruckModel FH = new(0, nameof(FH));
        public static readonly TruckModel FM = new(1, nameof(FM));

        public TruckModel(int id, string name) : base(id, name)
        {
        }
    }
}
