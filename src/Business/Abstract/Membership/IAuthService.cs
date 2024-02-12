using Core.Utilities.Results;
using Entities.Dtos;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IAuthService
    {
        Task<IBaseResult> Login(LoginReqDto model);
    }
}
