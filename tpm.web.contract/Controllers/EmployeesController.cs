using Microsoft.AspNetCore.Mvc;

namespace tpm.web.contract.Controllers
{
    public class EmployeesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
