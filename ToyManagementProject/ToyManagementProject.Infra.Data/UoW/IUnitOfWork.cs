using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToyManagementProject.Domain.Interfaces.Repositories;

namespace ToyManagementProject.Infra.Data.UoW
{
	public interface IUnitOfWork : IAsyncDisposable
	{
		Task BeginTransactionAsync();
		Task CommitAsync();
		Task RollbackAsync();
	}
}
