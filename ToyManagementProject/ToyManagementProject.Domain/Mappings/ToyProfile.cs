using AutoMapper;
using ToyManagementProject.Domain.DTOs;
using ToyManagementProject.Domain.Entities;

namespace ToyManagementProject.Domain.Mappings
{
	public class ToyProfile: Profile
	{
        public ToyProfile()
        {
			CreateMap<Toy, ToyDTO>();
			CreateMap<ToyDTO, Toy>();
		}
    }
}
