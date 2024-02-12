using Business.Abstract.Department;
using Core.Utilities.Results;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers.Department
{
    public class DepartmentController(IDepartmentService service) : PublicController
    {
        private readonly IDepartmentService _service = service;

        [HttpPost]
        public IDataResult<TDepartment> Create(TDepartment model) => _service.Create(model);
        [HttpGet]
        public IDataResult<List<TDepartment>> GetList(TDepartment model) => _service.GetList();
    }
}