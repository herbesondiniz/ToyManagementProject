
namespace ToyManagementProject.Services.Validators.Interfaces
{
	public interface IValidator<TEntity> where TEntity : class
	{
		IEnumerable<string> Validate(TEntity obj);
	}
}
