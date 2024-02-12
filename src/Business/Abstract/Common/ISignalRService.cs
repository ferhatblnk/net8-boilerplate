using Core.Utilities.Results;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ISignalRService
    {
        Task<IBaseResult> SendAll(string message);
        Task<IBaseResult> SendAllExcept(IReadOnlyList<string> excludedConnectionIds, string message);
        Task<IBaseResult> SendClient(string connectionId, string message);
        Task<IBaseResult> SendClients(IReadOnlyList<string> connectionIds, string message);
        Task<IBaseResult> SendGroup(string groupName, string message);
        Task<IBaseResult> SendGroupExcept(string groupName, IReadOnlyList<string> excludedConnectionIds, string message);
        Task<IBaseResult> SendGroups(IReadOnlyList<string> groupNames, string message);
        Task<IBaseResult> SendUser(string userId, string message);
        Task<IBaseResult> SendUsers(IReadOnlyList<string> userIds, string message);
    }
}
