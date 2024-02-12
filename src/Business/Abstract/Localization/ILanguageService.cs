using Core.Utilities.Results;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ILanguageService
    {
        Task<IBaseResult> GetAll();
        void GetAllReset();
    }
}
