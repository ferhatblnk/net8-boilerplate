using Business.Abstract;
using Core.Utilities.Results;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class SignalRManager : BaseManager, ISignalRService
    {
        public Task<IBaseResult> SendAll(string message)
        {
            _hubService.Clients.All.SendMessage(message);

            return Task.FromResult<IBaseResult>(new SuccessResult());
        }
        public Task<IBaseResult> SendAllExcept(IReadOnlyList<string> excludedConnectionIds, string message)
        {
            _hubService.Clients.AllExcept(excludedConnectionIds).SendMessage(message);

            return Task.FromResult<IBaseResult>(new SuccessResult());
        }
        public Task<IBaseResult> SendClient(string connectionId, string message)
        {
            _hubService.Clients.Client(connectionId).SendMessage(message);

            return Task.FromResult<IBaseResult>(new SuccessResult());
        }
        public Task<IBaseResult> SendClients(IReadOnlyList<string> connectionIds, string message)
        {
            _hubService.Clients.Clients(connectionIds).SendMessage(message);

            return Task.FromResult<IBaseResult>(new SuccessResult());
        }
        public Task<IBaseResult> SendGroup(string groupName, string message)
        {
            _hubService.Clients.Group(groupName).SendMessage(message);

            return Task.FromResult<IBaseResult>(new SuccessResult());
        }
        public Task<IBaseResult> SendGroupExcept(string groupName, IReadOnlyList<string> excludedConnectionIds, string message)
        {
            _hubService.Clients.GroupExcept(groupName, excludedConnectionIds).SendMessage(message);

            return Task.FromResult<IBaseResult>(new SuccessResult());
        }
        public Task<IBaseResult> SendGroups(IReadOnlyList<string> groupNames, string message)
        {
            _hubService.Clients.Groups(groupNames).SendMessage(message);

            return Task.FromResult<IBaseResult>(new SuccessResult());
        }
        public Task<IBaseResult> SendUser(string userId, string message)
        {
            _hubService.Clients.User(userId).SendMessage(message);

            return Task.FromResult<IBaseResult>(new SuccessResult());
        }
        public Task<IBaseResult> SendUsers(IReadOnlyList<string> userIds, string message)
        {
            _hubService.Clients.Users(userIds).SendMessage(message);

            return Task.FromResult<IBaseResult>(new SuccessResult());
        }
    }
}
