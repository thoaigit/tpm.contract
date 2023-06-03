using Microsoft.AspNetCore.Mvc;

namespace tpm.web.contract.Controllers
{
    public class ServicesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
