using AutoMapper;
using GarageVolver.Domain.Entities;
using GarageVolver.Domain.Interfaces;

namespace GarageVolver.Service.Services
{
    public class TruckService : BaseService<Truck>, ITruckService
    {
        public TruckService(
            ITruckRepository truckRepository,
            IMapper mapper
        ) : base(truckRepository, mapper)
        {
        }
    }
}
