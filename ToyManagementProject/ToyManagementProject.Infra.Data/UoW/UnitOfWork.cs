using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ToyManagementProject.Domain.Interfaces.Repositories;
using ToyManagementProject.Infra.Data.Context;
using ToyManagementProject.Infra.Data.RepoEF;

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
			await _dbContext.SaveChangesAsync();
		}

		public async Task Rollback()
		{			
		}		
	}
}
