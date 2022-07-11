using AutoMapper;
using GarageVolver.API.Models;
using GarageVolver.Domain.Entities;
using GarageVolver.Domain.Helpers;

namespace GarageVolver.API.Configurations
{
    public class TruckMapProfile : Profile
    {
        public TruckMapProfile()
        {
            CreateMap<CreateTruckModel, Truck>()
                .ConstructUsing(obj => new Truck(Enumeration.GetByName<TruckModel>(obj.ModelName), obj.ManufacturingYear, obj.ModelYear))
                .ForMember(dest => dest.Model, opt => opt.ConvertUsing(new StringToTruckModelConverter(), src => src.ModelName));
            CreateMap<UpdateTruckModel, Truck>()
                .ConstructUsing(obj => new Truck(Enumeration.GetByName<TruckModel>(obj.ModelName), obj.ManufacturingYear, obj.ModelYear) { Id = obj.Id })
                .ForMember(dest => dest.Model, opt => opt.ConvertUsing(new StringToTruckModelConverter(), src => src.ModelName));
            CreateMap<GetTruckModel, Truck>()
                .ConstructUsing(obj => new Truck(Enumeration.GetByName<TruckModel>(obj.ModelName), obj.ManufacturingYear, obj.ModelYear) { Id = obj.Id })
                .ForMember(dest => dest.Model, opt => opt.ConvertUsing(new StringToTruckModelConverter(), src => src.ModelName));
            CreateMap<Truck, GetTruckModel>()
                .ConstructUsing(obj => new GetTruckModel(obj.Id, obj.Model.Name, obj.ManufacturingYear, obj.ModelYear))
                .ForMember(dest => dest.ModelName, opt => opt.ConvertUsing(new TruckModelToStringConverter(), src => src.Model));
        }
    }

    public class TruckModelToStringConverter : IValueConverter<TruckModel, string>
    {
        public string Convert(TruckModel source, ResolutionContext context)
            => source.ToString();
    }

    public class StringToTruckModelConverter : IValueConverter<string, TruckModel>
    {
        public TruckModel Convert(string source, ResolutionContext context)
            => Enumeration.GetByName<TruckModel>(source);
    }
}
