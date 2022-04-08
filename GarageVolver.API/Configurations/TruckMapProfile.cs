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
                .ForMember(dest => dest.Model, opt => opt.ConvertUsing(new StringToTruckModelConverter(), src => src.ModelName));
            CreateMap<UpdateTruckModel, Truck>()
                .ForMember(dest => dest.Model, opt => opt.ConvertUsing(new StringToTruckModelConverter(), src => src.ModelName));
            CreateMap<GetTruckModel, Truck>()
                .ForMember(dest => dest.Model, opt => opt.ConvertUsing(new StringToTruckModelConverter(), src => src.ModelName));
            CreateMap<Truck, GetTruckModel>()
                .ForMember(dest => dest.ModelName, opt => opt.ConvertUsing(new TruckModelToStringConverter(), src => src.Model));
            CreateMap<GetTruckModel, UpdateTruckModel>();
            CreateMap<GetTruckModel, CreateTruckModel>();
            CreateMap<UpdateTruckModel, GetTruckModel>();
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
