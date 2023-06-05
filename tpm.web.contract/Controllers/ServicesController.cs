using CoC.Business.DTO;
using Core.DTO.Response;
using Microsoft.AspNetCore.Mvc;
using tpm.business;

namespace tpm.web.contract.Controllers
{
    public class ServicesController : Controller
    {
        private readonly IServiceService _serviceService;
        private readonly ServiceCreateReqValidator _validator;
        public ServicesController(IServiceService serviceService, ServiceCreateReqValidator validator)
        {
            _serviceService = serviceService;
            _validator = validator;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(ServiceCreateReq serviceReq)
        {
            var validationResult = _validator.Validate(serviceReq);

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }

                return View(serviceReq);
            }

            var result = _serviceService.Create(serviceReq);

            if (result.StatusCode == CRUDStatusCodeRes.Success)
            {
                return RedirectToAction("GetAll", "Service");
            }

            ModelState.AddModelError("", "Có lỗi xảy ra khi tạo dịch vụ.");

            return View(serviceReq);
        }
    }
}
