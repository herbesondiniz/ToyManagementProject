using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToyManagementProject.Domain
{
	public class Result<TEntity> where TEntity : class
	{
		public bool IsSuccess { get; private set; }
		public TEntity Data { get; private set; }
		public string Message { get; private set; }
		public IReadOnlyCollection<string> Errors { get; private set; }

		private List<string> _errors;

		private Result(bool isSuccess, TEntity data, IEnumerable<string> errors, string message = "")
		{
			IsSuccess = isSuccess;
			Data = data;
			_errors = errors.ToList() ?? new List<string>();
			Errors = _errors.AsReadOnly();
			Message = message;
		}
		public static Result<TEntity> Success(TEntity data, string message="") =>
			new Result<TEntity>(true, data, new List<string>(), message);
		public static Result<TEntity> Failure(IEnumerable<string> errors) =>
			new Result<TEntity>(false, null, errors);
		public static Result<TEntity> Failure(string error) =>
			new Result<TEntity>(false, null, new List<string> { error });

	}
}
