using ToyManagementProject.Domain.Entities;
using ToyManagementProject.Services.Dtos;

namespace ToyManagementProject.Services.Mappings
{
	public class StockProfile: OrderProfile
	{
		public StockProfile()
		{
			CreateMap<Stock, StockDto>();
			CreateMap<StockDto, Stock>();
		}
	}
}
