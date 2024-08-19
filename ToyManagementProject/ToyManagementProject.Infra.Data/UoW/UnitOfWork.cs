using ToyManagementProject.Infra.Data.Context;

namespace ToyManagementProject.Infra.Data.UoW
{
	public class UnitOfWork: IUnitOfWork
	{		
		private readonly ToyDbContext _dbContext;
		public UnitOfWork(ToyDbContext context)
		{
			_dbContext = context;
		}
		public async Task Commit()
		{
			try
			{
				await _dbContext.SaveChangesAsync();
			}
			catch (Exception)
			{
				throw;
			}						
		}
	
		public async Task Rollback()
		{			
		}
		public void Dispose()
		{
			_dbContext?.Dispose();
		}
	}
}
