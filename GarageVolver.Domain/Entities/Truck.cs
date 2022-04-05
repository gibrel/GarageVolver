namespace GarageVolver.Domain.Entities
{
    public class Truck : BaseEntity
    {
        public TruckModel Model { get; set; }
        public int ManufacturingYear { get; set; } = DateTime.Now.Year;
        public int ModelYear { get; set; }
    }
}
