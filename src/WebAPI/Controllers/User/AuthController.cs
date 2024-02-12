using Business.Abstract;
using Core.Utilities.Results;
using Entities.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    public class AuthController(IAuthService service) : PublicController
    {
        private readonly IAuthService _service = service;

        [HttpPost]
        public Task<IBaseResult> Login(LoginReqDto model) => _service.Login(model);
    }
}
