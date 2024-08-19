﻿using ToyManagementProject.Domain.Entities;

namespace ToyManagementProject.Domain.Interfaces.Repositories
{
	public interface IStockRepository: IRepositoryBase<Stock>
	{
		Task<Stock> GetStockByToyIdAsync(int toyId);
	}
}
