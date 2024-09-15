using ToyManagementProject.Domain.Entities;
using ToyManagementProject.Services.Dtos.Stock;

namespace ToyManagementProject.Services.Mappings
{
    public class StockProfile: OrderProfile
	{
		public StockProfile()
		{
			CreateMap<Stock, StockDto>();
			CreateMap<StockDto, Stock>();

			CreateMap<Stock, CreateStockDto>();
			CreateMap<CreateStockDto, Stock>();

			CreateMap<Stock, UpdateStockDto>();
			CreateMap<UpdateStockDto, Stock>();
		}
	}
}
