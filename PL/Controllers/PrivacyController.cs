using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
namespace PL.Controllers
{
    [Authorize]
    public class PrivacyController : Controller
    {
        
        public IActionResult Index()
        {
          ViewBag.Message = "No cuenta con el permiso para esta opcion";
          return RedirectToAction("GetAll", "Empleado");

        }
    }
}
