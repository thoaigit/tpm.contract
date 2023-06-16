using Core.DTO.Response;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using tpm.business;
using tpm.dto.admin;
using Microsoft.AspNetCore.Mvc.Rendering;
using Dapper;
using System;
using tpm.dto.admin.Response;

namespace tpm.web.contract.Controllers
{
    public class ServicesController : Controller
    {
        private readonly IServiceService _serviceService;
        private readonly ServiceCreateReqValidator _validator;
        private readonly IServiceTypeService _serviceTypeService;
        private readonly IUnitService _serviceUnit;
        public ServicesController(IServiceService serviceService, ServiceCreateReqValidator validator, IServiceTypeService serviceTypeService, IUnitService serviceUnit)
        {
            _serviceService = serviceService;
            _validator = validator;
            _serviceTypeService = serviceTypeService;
            _serviceUnit = serviceUnit;
        }
        [HttpGet]
        public IActionResult GetService(int Service_ID)
        {
            var getService = _serviceService.GetServicesByID(Service_ID);

            return Json(new { Service = getService });
        }
        public IActionResult Index()
        {
            var services = _serviceService.GetServicesWithTypeName();
            var serviceTypes = _serviceTypeService.GetAllServiceTypes();
            var unit = _serviceUnit.GetAllUnits();

            ViewBag.Services = services;
            ViewBag.ServiceTypes = serviceTypes;
            ViewBag.Unit = unit;

            return View();
        }
        public IActionResult Create()
        {
            return View();
        }
    }
}
