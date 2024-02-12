using Business.Hubs.Abstract;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Business.Hubs.Concrete
{
    public class HubService : Hub<IHubService>
    {
        public async Task SendMessage(string data) => await Clients.All.SendMessage(data);
    }
}