using ToyManagementProject.Domain.Entities;
using ToyManagementProject.Domain.Interfaces.Repositories;
using ToyManagementProject.Infra.Data.RepoEF;
using Moq;

namespace ToyManagementProject.Tests.Repositories
{
	public class OrderRepositoryTests
	{
		private readonly Mock<IRepositoryBase<Order>> _repositoryBaseMock;
		private readonly OrderRepository _orderRepository;
		public OrderRepositoryTests()
		{
			_repositoryBaseMock = new Mock<IRepositoryBase<Order>>();
			_orderRepository = new OrderRepository(_repositoryBaseMock.Object);			
		}
		[Fact]
		public async Task AddAsync_Should_Call_AddAsync_Once()
		{		
			var order = new Order();

			await _orderRepository.AddAsync(order);

			_repositoryBaseMock.Verify(r => r.AddAsync(order), Times.Once);
		}
		[Fact]
		public async Task DeleteAsync_Should_Call_DeleteAsync_Once()
		{
			var orderId = 1;

			await _orderRepository.DeleteAsync(orderId);

			_repositoryBaseMock.Verify(r => r.DeleteAsync(orderId), Times.Once);
		}

		[Fact]
		public async Task GetAllAsync_Should_Return_List_Of_Orders()
		{
			// Arrange
			var orders = new List<Order> { new Order(), new Order() };
			_repositoryBaseMock.Setup(r => r.GetAllAsync()).ReturnsAsync(orders);

			// Act
			var result = await _orderRepository.GetAllAsync();

			// Assert
			Assert.Equal(orders, result);
		}

		[Fact]
		public async Task GetByIdAsync_Should_Return_Order_With_Correct_Id()
		{
			// Arrange
			var orderId = 1;
			var expectedOrder = new Order { Id = orderId };
			_repositoryBaseMock.Setup(r => r.GetByIdAsync(orderId)).ReturnsAsync(expectedOrder);

			// Act
			var result = await _orderRepository.GetByIdAsync(orderId);

			// Assert
			Assert.Equal(expectedOrder, result);
		}
		[Fact]
		public async Task UpdateAsync_Should_Call_UpdateAsync_Once()
		{
			// Arrange
			var order = new Order();

			// Act
			await _orderRepository.UpdateAsync(order);

			// Assert
			_repositoryBaseMock.Verify(r => r.UpdateAsync(order), Times.Once);
		}
	}
}