using System.Threading.Tasks;
using Cqrs.Template.Domain.SeedWork;
using Cqrs.Template.Infra.Data.Context;

namespace Cqrs.Template.Infra.Data.UnitOfWork
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly ApplicationDbContext _applicationDbContext;

		public UnitOfWork(ApplicationDbContext applicationDbContext)
		{
			_applicationDbContext = applicationDbContext;
		}

		public async Task<bool> Commit()
		{
			return await _applicationDbContext.SaveEntitiesAsync();
		}

		public void Dispose()
		{
			_applicationDbContext.Dispose();
		}
	}
}