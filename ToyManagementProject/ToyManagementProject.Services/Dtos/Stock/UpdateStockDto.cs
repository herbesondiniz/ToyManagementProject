
namespace ToyManagementProject.Services.Dtos.Stock
{
    public class UpdateStockDto
    {
        public int Id { get; set; }
        public int ToyId { get; set; }
        public int Quantity { get; set; }
    }
}
