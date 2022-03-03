using System.Threading.Tasks;

namespace Cqrs.Template.Domain.SeedWork;

public interface IUnitOfWork
{
	Task<bool> CommitAsync();
}
