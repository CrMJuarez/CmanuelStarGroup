using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
namespace PL.Controllers
{
    [Authorize]
    public class AreaController : Controller
    {
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Area area = new ML.Area();
            ML.Result result = BL.Area.GetAll();
            if (result.Correct)
            {
                area.Areas = result.Objects.ToList();
                return View(area);
            }
            else
                ViewBag.Message = "ocurrio un problema" + result.ErrorMessage;
            return PartialView("Modal");
        }

        [HttpGet]
        public ActionResult Form(int? IdArea)
        {
            ML.Area area = new ML.Area();

            if (IdArea == null)
            {


                return View(area);
            }
            else
            {
                ML.Result result = BL.Area.GetById(IdArea.Value);
                if (result.Correct)
                {
                    area = (ML.Area)result.Object;


                    return View(area);
                }
                else
                {
                    ViewBag.Message = result.ErrorMessage;
                    return View();
                }
            }
        }

        [HttpPost]
        public ActionResult Form(ML.Area area)
        {
            if (area.IdArea == null)
            {

                ML.Result result = BL.Area.Add(area);
                if (result.Correct)
                {
                    ViewBag.Message = "Se agrego correctamente el area";
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
                ML.Result result = BL.Area.Update(area);
                if (result.Correct)
                {
                    ViewBag.Message = "Se actualizo correctamente el area";
                    return PartialView("Modal");
                }
                else
                {
                    ViewBag.Message = "No se pudo actualizar el area";
                    return PartialView("Modal");

                }
            }

        }
        [HttpGet]
        public ActionResult Delete(int IdArea)
        {
            ML.Area area = new ML.Area();
            area.IdArea = IdArea;
            var result = BL.Area.Delete(area);

            if (result.Correct)
            {
                ViewBag.Message = "Se elimino correctamente el area";

            }
            else
            {
                ViewBag.Message = "ocurrio un problema" + result.ErrorMessage;

            }

            return PartialView("Modal");


        }
    }
}
