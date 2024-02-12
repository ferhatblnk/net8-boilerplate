using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IFileService
    {
        Task<string> Save(IFormFile file);
    }
}
