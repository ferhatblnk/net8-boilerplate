using Business.Abstract.Role;
using Core.Utilities.Results;
using Entities.Concrete.Membership;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers.Role
{
    public class RoleController(IRoleService service) : PublicController
    {
        private readonly IRoleService _service = service;

        [HttpPost]
        public IDataResult<TRole> Create(TRole model) => _service.Create(model);
        [HttpGet]
        public IDataResult<List<TRole>> GetList(TRole model) => _service.GetList();
    }
}