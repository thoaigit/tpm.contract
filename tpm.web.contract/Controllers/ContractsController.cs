using Core.DTO.Response;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using tpm.business;
using tpm.dto.admin;
using Microsoft.AspNetCore.Mvc.Rendering;
using Dapper;
using System;

namespace tpm.web.contract.Controllers
{
    public class ContractsController : Controller
    {   
        private readonly IServiceService _serviceService;
        private readonly ServiceCreateReqValidator _validator;
        private readonly IServiceTypeService _serviceTypeService;
        public ContractsController(IServiceService serviceService, ServiceCreateReqValidator validator, IServiceTypeService serviceTypeService)
        {
            _serviceService = serviceService;
            _validator = validator;
            _serviceTypeService = serviceTypeService;
        }
        [MvcAuthorize]
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {
            var services = _serviceService.GetServicesWithTypeName();
            ViewBag.Services = services;           
            return View();
        }

        public IActionResult ServiceDetail()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Create(ServiceCreateReq obj)
        {
            try
            {
                var serviceTypes = _serviceTypeService.GetAllServiceTypes();
                ViewBag.ServiceTypes = serviceTypes;
                var result = _serviceService.Create(obj);

                if (result.StatusCode == CRUDStatusCodeRes.Success)
                {
                    return RedirectToAction("Create"); // Điều hướng đến action "Index" sau khi tạo thành công
                }

                ModelState.AddModelError(string.Empty, result.ErrorMessage);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Có lỗi xảy ra");
            }

            return View(obj); // Trả về view với thông tin đối tượng đã nhập và thông báo lỗi
        }
    }
}

