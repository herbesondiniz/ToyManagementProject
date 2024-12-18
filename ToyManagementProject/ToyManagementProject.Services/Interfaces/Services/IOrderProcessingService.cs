﻿using ToyManagementProject.Domain.Entities;

namespace ToyManagementProject.Domain.Interfaces.Services
{
	public interface IOrderProcessingService
	{
		Task<Result<object>> ProcessOrderStockAsync(Order order);
	}
}
