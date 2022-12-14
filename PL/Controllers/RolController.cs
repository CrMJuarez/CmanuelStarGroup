using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
namespace PL.Controllers
{
    [Authorize]
    public class RolController : Controller
    {

        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Rol rol = new ML.Rol();
            ML.Result result = BL.Rol.GetAll();
            if (result.Correct)
            {
                rol.Roles = result.Objects.ToList();
                return View(rol);
            }
            else
                ViewBag.Message = "ocurrio un problema" + result.ErrorMessage;
            return PartialView("Modal");
        }

        [HttpGet]
        public ActionResult Form(int? IdRol)
        {
            ML.Rol rol = new ML.Rol();

            if (IdRol == null)
            {


                return View(rol);
            }
            else
            {
                ML.Result result = BL.Rol.GetById(IdRol.Value);
                if (result.Correct)
                {
                    rol = (ML.Rol)result.Object;


                    return View(rol);
                }
                else
                {
                    ViewBag.Message = result.ErrorMessage;
                    return View();
                }
            }
        }

        [HttpPost]
        public ActionResult Form(ML.Rol rol)
        {
            if (rol.IdRol == null)
            {

                ML.Result result = BL.Rol.Add(rol);
                if (result.Correct)
                {
                    ViewBag.Message = "Se agrego correctamente el rol";
                    return PartialView("Modal");
                }
                else
                {
                    ViewBag.Message = "ocurrio un problema" + result.ErrorMessage;
                    return PartialView("Modal");
                }
            }
            else
            {
                ML.Result result = BL.Rol.Update(rol);
                if (result.Correct)
                {
                    ViewBag.Message = "Se actualizo correctamente el rol";
                    return PartialView("Modal");
                }
                else
                {
                    ViewBag.Message = "No se pudo actualizar el rol";
                    return PartialView("Modal");

                }
            }

        }
        [HttpGet]
        public ActionResult Delete(int IdRol)
        {
            ML.Rol rol = new ML.Rol();
            rol.IdRol = IdRol;
            var result = BL.Rol.Delete(rol);

            if (result.Correct)
            {
                ViewBag.Message = "Se elimino correctamente el rol";

            }
            else
            {
                ViewBag.Message = "ocurrio un problema" + result.ErrorMessage;

            }

            return PartialView("Modal");


        }
    }
}

