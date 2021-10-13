using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Cqrs.Template.Application.EventHandlers
{
	public abstract class EventHandler<T> : INotificationHandler<T> where T : INotification
	{
		protected EventHandler()
		{
			
		}

		public abstract Task Handle(T notification, CancellationToken cancellationToken);
	}
}