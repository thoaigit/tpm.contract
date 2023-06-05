using Microsoft.AspNetCore.Mvc;

namespace tpm.web.contract.Controllers
{
    public class ContractsController : Controller
    {
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
    }
}
