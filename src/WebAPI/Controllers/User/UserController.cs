using Business.Abstract;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.Dtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers.User
{
    public class UserController(IUserService service) : PublicController
    {
        private readonly IUserService _service = service;

        [HttpPost]
        public IDataResult<UserResDto> Create(UserDto model) => _service.Create(model);
        [HttpGet]
        public IDataResult<IList<UserResDto>> GetList()
        {
            var ss = _service.GetAll();
            return ss;
        }
        [HttpGet]
        public IDataResult<IList<UserResDto>> GetUserByToken(string token)
        {
            var ss = _service.GetAll();
            return ss;
        }

    }
}