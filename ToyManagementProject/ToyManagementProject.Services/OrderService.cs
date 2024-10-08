﻿using ToyManagementProject.Domain;
using ToyManagementProject.Domain.Entities;
using ToyManagementProject.Domain.Interfaces.Repositories;
using ToyManagementProject.Domain.Interfaces.Services;
using ToyManagementProject.Infra.Data.UoW;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ToyManagementProject.Services.Dtos.Order;

namespace ToyManagementProject.Services
{
	public class OrderService : IOrderService
	{
		private readonly IOrderRepository _orderRepository;
		private readonly IOrderItemService _orderItemService;
		private readonly IOrderProcessingService _orderProcessingService;
		//private readonly IValidator<Order> _orderValidator;
		private readonly IUnitOfWork _uow;
		private readonly IMapper _mapper;

		public OrderService(IOrderRepository orderRepository,
							IOrderItemService orderItemService,
							IOrderProcessingService orderProcessingService,
							//IValidator<Order> orderValidator,
							IUnitOfWork uow,
							IMapper mapper
							)
		{
			_orderRepository = orderRepository;
			_orderItemService = orderItemService;
			_orderProcessingService = orderProcessingService;
			//_orderValidator = orderValidator;
			_uow = uow;
			_mapper = mapper;
		}

		public async Task<Result<OrderDto>> AddAsync(Order order)
		{
			if (!order.IsValid())
			{
				return Result<OrderDto>.Failure(order.ErrorsNotifications);
			}

			var processResult = await _orderProcessingService.ProcessOrderAsync(order);

			if (!processResult.IsSuccess)
			{
				return Result<OrderDto>.Failure(processResult.Errors);
			}

			try
			{
				await _orderRepository.AddAsync(order);

				await _uow.CommitAsync();

				return Result<OrderDto>.Success(_mapper.Map<OrderDto>(order));
			}
			catch (Exception ex)
			{				
				return Result<OrderDto>.Failure(new List<string> { $"An error occurred while processing the order: {ex.Message} " });
			}
		}

		public async Task<Result<IEnumerable<OrderDto>>> GetAllAsync()
		{
			var orders = await _orderRepository.GetAllAsync(q => q.Include(o=>o.Items));

			if (orders == null || orders.Count == 0)
			{
				return Result<IEnumerable<OrderDto>>.Failure("Orders list is empty");
			}			

			var ordersDTO = _mapper.Map<IEnumerable<OrderDto>>(orders);

			return Result<IEnumerable<OrderDto>>.Success(ordersDTO);

		}
		public async Task<Result<OrderDto>> GetByIdAsync(int id)
		{
			var order = await _orderRepository.GetByIdAsync(id, q=> q.Include(o=> o.Items));
			
			if (order == null)
			{
				return Result<OrderDto>.Failure("Order doesn`t exists");
			}			

			return Result<OrderDto>.Success(_mapper.Map<OrderDto>(order));
		}

		public async Task<Result<OrderDto>> DeleteAsync(int id)
		{
			var order = await _orderRepository.GetByIdAsync(id);

			if (order == null)
			{
				return Result<OrderDto>.Failure($"Order doesn´t exists");
			}
		
			try
			{
				await _orderRepository.DeleteAsync(id);

				await _uow.CommitAsync();

				return Result<OrderDto>.Success(new OrderDto());
			}
			catch (Exception ex)
			{
				return Result<OrderDto>.Failure($"Error DeleteAsync: {ex.Message}");
			}

		}
		public async Task<Result<OrderDto>> UpdateAsync(Order order)
		{
			if (!order.IsValid())
			{
				return Result<OrderDto>.Failure(order.ErrorsNotifications);
			}

			try
			{
				await _orderRepository.UpdateAsync(order);

				await _uow.CommitAsync();

				return Result<OrderDto>.Success(null, "Toy updated");
			}
			catch (Exception ex)
			{
				return Result<OrderDto>.Failure(new List<string> { $"Error UpdateAsync: {ex.Message}" });
				throw;
			}
		}		
	}
}
