using Microsoft.AspNetCore.Mvc;

namespace tpm.web.contract.Controllers
{
    public class ContactsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }
    }
}
