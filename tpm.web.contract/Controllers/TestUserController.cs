﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using tpm.business;
using tpm.web.contract.Models;

namespace tpm.web.contract.Controllers
{
    public class TestUserController : Controller
    {
        private readonly ITestUserService _testUserService;
        private readonly IServiceTypeService _serviceTypeService;

        public TestUserController(ITestUserService testUserService, IServiceTypeService serviceTypeService)
        {
            _testUserService = testUserService;
            _serviceTypeService = serviceTypeService;
        }

        public IActionResult GetAll()
        {
            var list = _testUserService.ReadAll();
            ViewBag.ObjList = list;
            return View();
        }
        public IActionResult GetAllTypeService()
        {
            var services = _serviceTypeService.GetAllServiceTypes();
            ViewBag.Services = services;
            return View();
            
        }
    }
}
