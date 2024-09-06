using Microsoft.EntityFrameworkCore.Storage;
using ToyManagementProject.Infra.Data.Context;

namespace ToyManagementProject.Infra.Data.UoW
{
	public class UnitOfWork: IUnitOfWork
	{		
		private readonly ToyDbContext _dbContext;
		private IDbContextTransaction _transaction;
		public UnitOfWork(ToyDbContext context)
		{
			_dbContext = context;
		}
		public async Task BeginTransactionAsync()
		{
			_transaction = await _dbContext.Database.BeginTransactionAsync();
		}
		public async Task CommitAsync()
		{
			if (_transaction != null) 
			{
				try
				{
					await _dbContext.SaveChangesAsync();
					//await _transaction.CommitAsync();
				}
				catch
				{
					await RollbackAsync();
					throw;
				}
				finally
				{
					//await _transaction.DisposeAsync();
				}
			}
			else
				await _dbContext.SaveChangesAsync();
		}
	
		public async Task RollbackAsync()
		{
			if (_transaction != null)
			{
				await _transaction.RollbackAsync();
				await _transaction.DisposeAsync();
			}
		}		
		public async ValueTask DisposeAsync()
		{
			if (_dbContext != null)
			{
				await _dbContext.DisposeAsync();
			}
		}
	}
}
