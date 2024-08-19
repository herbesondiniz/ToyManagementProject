using Microsoft.AspNetCore.Mvc;
using Moq;
using ToyManagementProject.API.Controllers;
using ToyManagementProject.Domain.Entities;
using ToyManagementProject.Domain.Interfaces.Services;
using ToyManagementProject.Infra.Data.UoW;

namespace ToyManagementProject.Tests.Controllers
{
	public class OrderControllerTests
	{
		private readonly Mock<IOrderService> _orderServiceMock;
		private readonly Mock<IToyService> _toyServiceMock;
		private readonly Mock<IOrderItemService> _orderItemServiceMock;
		private readonly Mock<IStockService> _stockServiceMock;
		private readonly Mock<IUnitOfWork> _uowMock;

		private readonly OrderController _controller;
		public OrderControllerTests()
		{
			_orderServiceMock = new Mock<IOrderService>();
			_toyServiceMock = new Mock<IToyService>();
			_orderItemServiceMock = new Mock<IOrderItemService>();
			_stockServiceMock = new Mock<IStockService>();
			_uowMock = new Mock<IUnitOfWork>();

			_controller = new OrderController(_orderServiceMock.Object,
											  _toyServiceMock.Object,
											  _orderItemServiceMock.Object,
											  _stockServiceMock.Object,
											  _uowMock.Object);
		}

		[Fact]
		public async Task GetAll_Should_Return_Ok_With_List_Of_Orders()
		{
			// Arrange
			var orders = new List<Order> { new Order(), new Order() };
			_orderServiceMock.Setup(s => s.GetAllAsync()).ReturnsAsync(orders);

			// Act
			var result = await _controller.GetAll();

			// Assert
			var okResult = Assert.IsType<OkObjectResult>(result.Result);
			Assert.Equal(200, okResult.StatusCode);
			Assert.Equal(orders, okResult.Value);
		}

		[Fact]
		public async Task GetById_Should_Return_Ok_With_Order_When_Order_Exists()
		{
			// Arrange
			var orderId = 1;
			var order = new Order { Id = orderId };
			_orderServiceMock.Setup(s => s.GetByIdAsync(orderId)).ReturnsAsync(order);

			// Act
			var result = await _controller.GetById(orderId);

			// Assert
			var okResult = Assert.IsType<OkObjectResult>(result.Result);
			Assert.Equal(200, okResult.StatusCode);
			Assert.Equal(order, okResult.Value);
		}
		[Fact]
		public async Task GetById_Should_Return_NotFound_When_Order_Does_Not_Exist()
		{
			// Arrange
			var orderId = 1;
			_orderServiceMock.Setup(s => s.GetByIdAsync(orderId)).ReturnsAsync((Order)null);

			// Act
			var result = await _controller.GetById(orderId);

			// Assert
			var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);
			Assert.Equal(404, notFoundResult.StatusCode);
			Assert.Equal("Order not found", notFoundResult.Value);
		}

		[Fact]
		public async Task Create_Should_Return_CreatedAtAction_When_Order_Is_Valid()
		{
			// Arrange
			var orderItems = new List<OrderItem>() { new OrderItem() { Id = 1, OrderId = 1, Price = 50, Quantity = 1 } };
			var order = new Order { Id = 1, ClientId = 1, Items = orderItems };

			_orderServiceMock.Setup(s => s.ProcessOrderAsync(order)).ReturnsAsync(true);
			_uowMock.Setup(u => u.Commit()).Returns(Task.CompletedTask);

			// Act
			var result = await _controller.Create(order);

			// Assert
			var createdResult = Assert.IsType<CreatedAtActionResult>(result);
			Assert.Equal(201, createdResult.StatusCode);
			Assert.Equal(order, createdResult.Value);
		}
		[Fact]
		public async Task Create_Should_Return_NotFound_When_Order_Is_Invalid()
		{
			// Arrange
			var orderItems = new List<OrderItem>() { new OrderItem() { Id = 1, OrderId = 1, Price = 50, Quantity = 1 } };
			var order = new Order { ClientId = 1, Items = orderItems };
			_orderServiceMock.Setup(s => s.ProcessOrderAsync(order)).ReturnsAsync(false);

			// Act
			var result = await _controller.Create(order);

			// Assert
			var notFoundResult = Assert.IsType<NotFoundResult>(result);
			Assert.Equal(404, notFoundResult.StatusCode);
		}
		[Fact]
		public async Task Update_Should_Return_NoContent_When_Order_Is_Updated()
		{
			// Arrange
			var orderItems = new List<OrderItem>() { new OrderItem() { Id = 1, OrderId = 1, Price = 50, Quantity = 1 } };
			var order = new Order { Id = 1, Items = orderItems };
			_orderServiceMock.Setup(s => s.UpdateAsync(order)).Returns(Task.CompletedTask);
			_uowMock.Setup(u => u.Commit()).Returns(Task.CompletedTask);

			// Act
			var result = await _controller.Update(order.Id, order);

			// Assert
			Assert.IsType<NoContentResult>(result);
		}

		[Fact]
		public async Task Update_Should_Return_BadRequest_When_Order_Id_Does_Not_Match()
		{
			// Arrange
			var order = new Order { Id = 1 };

			// Act
			var result = await _controller.Update(2, order);

			// Assert
			var badRequestResult = Assert.IsType<BadRequestResult>(result);
			Assert.Equal(400, badRequestResult.StatusCode);
		}
		[Fact]
		public async Task Delete_Should_Return_NoContent_When_Order_Is_Deleted()
		{
			// Arrange
			var orderId = 1;
			var order = new Order { Id = orderId };
			_orderServiceMock.Setup(s => s.GetByIdAsync(orderId)).ReturnsAsync(order);
			_orderServiceMock.Setup(s => s.DeleteAsync(orderId)).Returns(Task.CompletedTask);

			// Act
			var result = await _controller.Delete(orderId);

			// Assert
			Assert.IsType<NoContentResult>(result);
		}

		[Fact]
		public async Task Delete_Should_Return_NotFound_When_Order_Does_Not_Exist()
		{
			// Arrange
			var orderId = 1;
			_orderServiceMock.Setup(s => s.GetByIdAsync(orderId)).ReturnsAsync((Order)null);

			// Act
			var result = await _controller.Delete(orderId);

			// Assert
			var notFoundResult = Assert.IsType<NotFoundResult>(result);
			Assert.Equal(404, notFoundResult.StatusCode);
		}
	}
}
