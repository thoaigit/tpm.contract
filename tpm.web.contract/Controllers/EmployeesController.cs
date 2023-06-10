using Microsoft.AspNetCore.Mvc;
using tpm.business;
using tpm.dto.admin;

namespace tpm.web.contract.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IEmployeeService _employeeService;
       
        
        public EmployeesController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
         
        }
        public IActionResult Index()
        {
            var list = _employeeService.ReadAll();
            ViewBag.ObjList = list;
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }
        public IActionResult Detail()
        {
            return View();
        }
        public IActionResult Edit()
        {
            return View();
        }

    }
}
