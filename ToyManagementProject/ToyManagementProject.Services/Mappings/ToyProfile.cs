using AutoMapper;
using ToyManagementProject.Domain.Entities;
using ToyManagementProject.Services.Dtos.Toy;

namespace ToyManagementProject.Services.Mappings
{
    public class ToyProfile : Profile
	{
		public ToyProfile()
		{
			CreateMap<Toy, ToyDto>();
			CreateMap<ToyDto, Toy>();

			CreateMap<Toy, CreateToyDto>();
			CreateMap<CreateToyDto, Toy>();

			CreateMap<Toy, UpdateToyDto>();
			CreateMap<UpdateToyDto, Toy>();
		}
	}
}
