using Core.DTO.Response;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using tpm.business;
using tpm.dto.admin;

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
            return View();
        }
        public IActionResult ServiceDetail()
        {
            return View();
        }
        public IActionResult CreateService()
        {
            return View();
        }

        //[HttpPost]
        //public IActionResult CreateService(ServiceCreateReq serviceReq)
        //{
        //    var validationResult = _validator.Validate(serviceReq);

        //    if (!validationResult.IsValid)
        //    {
        //        foreach (var error in validationResult.Errors)
        //        {
        //            ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
        //        }

        //        return View(serviceReq);
        //    }

           
        //    var serviceType = _serviceTypeService.GetServiceTypeById(serviceReq.Service_Type_ID);
        //    if (serviceType == null)
        //    {
        //        ModelState.AddModelError("", "Không tìm thấy loại dịch vụ.");
        //        return View(serviceReq);
        //    }

          
        //    serviceReq.Name = serviceType.ServiceTypeName;

        //    var result = _serviceService.Create(serviceReq);

        //    if (result.StatusCode == CRUDStatusCodeRes.Success)
        //    {
        //        return RedirectToAction("GetAll", "Service");
        //    }

        //    ModelState.AddModelError("", "Có lỗi xảy ra khi tạo dịch vụ.");

        //    return View(serviceReq);
        //}
    }
}
