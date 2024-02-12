using Business.Abstract;
using Core.Utilities.Results;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    public class HealthController(IAuthService service) : PublicController
    {
        private readonly IAuthService _service = service;

        [HttpGet]
        public IBaseResult Check() => new SuccessResult();
    }
}
