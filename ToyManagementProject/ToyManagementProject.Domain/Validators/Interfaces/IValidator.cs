using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ToyManagementProject.Domain.Entities;

namespace ToyManagementProject.Domain.Validators.Interfaces
{
	public interface IValidator<TEntity> where TEntity : class
	{
		IEnumerable<string> Validate(TEntity obj);
	}
}
