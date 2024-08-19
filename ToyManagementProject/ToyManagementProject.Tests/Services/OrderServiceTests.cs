using ToyManagementProject.Domain.Entities;
using ToyManagementProject.Domain.Interfaces.Services;
using ToyManagementProject.Services;
using ToyManagementProject.Domain.Interfaces.Repositories;
using ToyManagementProject.Infra.Data.UoW;
using Moq;

namespace ToyManagementProject.Tests.Services
{
	public class OrderServiceTests
	{
		private readonly Mock<IRepositoryBase<Order>> _orderRepositoryMock;
		private readonly Mock<IOrderItemRepository> _orderItemRepositoryMock;
		private readonly Mock<IToyService> _toyServiceMock;
		private readonly Mock<IOrderItemService> _orderItemServiceMock;
		private readonly Mock<IStockService> _stockServiceMock;
		private readonly Mock<IUnitOfWork> _uowMock;

		private readonly OrderService _orderService;
        public OrderServiceTests()
        {
			_orderRepositoryMock = new Mock<IRepositoryBase<Order>>();
			_orderItemRepositoryMock = new Mock<IOrderItemRepository>();
			_toyServiceMock = new Mock<IToyService>();
			_orderItemServiceMock = new Mock<IOrderItemService>();
			_stockServiceMock = new Mock<IStockService>();
			_uowMock = new Mock<IUnitOfWork>();

			_orderService = new OrderService(_orderRepositoryMock.Object, 											 
											 _orderItemRepositoryMock.Object,
											 _toyServiceMock.Object,
											 _orderItemServiceMock.Object,
											 _stockServiceMock.Object,
											 _uowMock.Object
											 );
		}

        [Fact]		
		public async Task AddAsync_Should_Call_AddAsync_Once()
		{		
			var order = new Order();

			await _orderService.AddAsync(order);
			
			_orderRepositoryMock.Verify(r => r.AddAsync(order), Times.Once);
		}

		[Fact]
		public async Task DeleteAsync_Should_Call_DeleteAsync_Once()
		{			
			var orderId = 1;
			
			await _orderService.DeleteAsync(orderId);
		
			_orderRepositoryMock.Verify(r => r.DeleteAsync(orderId), Times.Once);
		}
		[Fact]
		public async Task GetByIdAsync_Should_Return_Order_With_Items()
		{
			// Arrange
			var orderId = 1;
			var order = new Order { Id = orderId };
			var items = new List<OrderItem> { new OrderItem { OrderId = orderId } };

			_orderRepositoryMock.Setup(r => r.GetByIdAsync(orderId)).ReturnsAsync(order);
			_orderItemRepositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(items);

			// Act
			var result = await _orderService.GetByIdAsync(orderId);

			// Assert
			Assert.Equal(orderId, result.Id);
			Assert.Equal(items, result.Items);
		}
		[Fact]
		public async Task ProcessOrderAsync_Should_Return_True_When_Order_Is_Valid_And_Stock_Is_Available()
		{
			// Arrange
			var order = new Order
			{
				Items = new List<OrderItem>
				{
					new OrderItem { ToyId = 1, Quantity = 1 }
				}
			};

			var toy = new Toy { Id = 1, Price = 100 };
			var stock = new Stock { ToyId = 1, Quantity = 10 };

			_toyServiceMock.Setup(t => t.GetByIdAsync(1)).ReturnsAsync(toy);
			_stockServiceMock.Setup(s => s.GetStockByToyIdAsync(1)).ReturnsAsync(stock);

			// Act
			var result = await _orderService.ProcessOrderAsync(order);

			// Assert
			Assert.True(result);
			_orderRepositoryMock.Verify(r => r.AddAsync(order), Times.Once);
			_orderItemServiceMock.Verify(i => i.UpdateAsync(It.IsAny<OrderItem>()), Times.Once);
			_stockServiceMock.Verify(s => s.UpdateAsync(stock), Times.Once);
		}
		[Fact]
		public async Task ProcessOrderAsync_Should_Return_False_When_Stock_Is_Not_Available()
		{
			// Arrange
			var order = new Order
			{
				Items = new List<OrderItem>
				{
					new OrderItem { ToyId = 1, Quantity = 1 }
				}
			};

			var toy = new Toy { Id = 1, Price = 100 };
			var stock = new Stock { ToyId = 1, Quantity = 0 }; // No stock available

			_toyServiceMock.Setup(t => t.GetByIdAsync(1)).ReturnsAsync(toy);
			_stockServiceMock.Setup(s => s.GetStockByToyIdAsync(1)).ReturnsAsync(stock);

			// Act
			var result = await _orderService.ProcessOrderAsync(order);

			// Assert
			Assert.False(result);
			_orderRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Order>()), Times.Never);
			_orderItemServiceMock.Verify(i => i.UpdateAsync(It.IsAny<OrderItem>()), Times.Never);
			_stockServiceMock.Verify(s => s.UpdateAsync(It.IsAny<Stock>()), Times.Never);
		}
	}
}
