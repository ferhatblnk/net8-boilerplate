using System.Threading.Tasks;

namespace Business.Hubs.Abstract
{
    public interface IHubService
    {
        Task SendMessage(string data);
    }
}