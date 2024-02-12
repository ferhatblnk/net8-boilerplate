using Business.Abstract;
using Core.Constants;
using Core.Extensions;
using Core.Infrastructure.Mapper;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class LanguageManager : BaseManager, ILanguageService
    {
        [YearlyCache]
        public Task<IBaseResult> GetAll()
        {
            var result = _languageDal.GetList(x => !x.Deleted)
                                .OrderBy(x => x.Name)
                                .ToList()
                                .Select(x => new LanguagesResDto()
                                {
                                    Id = x.Id,
                                    Value = x.Name,
                                    LanguageCode = x.LanguageCode
                                })
                                .ToList();

            return Task.FromResult<IBaseResult>(new SuccessDataResult<IList<LanguagesResDto>>(result));
        }
        [CacheRemove("ILanguageService.GetAll")]
        public void GetAllReset() { }
    }
}