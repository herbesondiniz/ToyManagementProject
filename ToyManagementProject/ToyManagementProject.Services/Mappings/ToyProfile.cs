using AutoMapper;
using ToyManagementProject.Domain.Entities;
using ToyManagementProject.Services.Dtos;

namespace ToyManagementProject.Services.Mappings
{
	public class ToyProfile : Profile
	{
		public ToyProfile()
		{
			CreateMap<Toy, ToyDto>();
			CreateMap<ToyDto, Toy>();
		}
	}
}
