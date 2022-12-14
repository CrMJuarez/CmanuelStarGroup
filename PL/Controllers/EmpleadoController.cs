using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
namespace PL.Controllers
{
    [Authorize]
    public class EmpleadoController : Controller
    {
        public ActionResult GetAll()
        {
            ML.Empleado empleado = new ML.Empleado();
            ML.Result result = BL.Empleado.GetAll();
            

            if (result.Correct)
            {
                empleado.Empleados = result.Objects.ToList();
                
                
                return View(empleado);
            }
            else
                ViewBag.Message = "ocurrio un problema" + result.ErrorMessage;
            return PartialView("Modal");
        }
        [Authorize(Roles = "Administrador")]
        
        [HttpGet]
        public ActionResult Form(int? IdEmpleado)
        {
            ML.Empleado empleado = new ML.Empleado();
            ML.Result resultArea = BL.Area.GetAll();
            empleado.Area = new ML.Area();
            if (IdEmpleado == null)
            {
                //usuario = (ML.Usuario)result.Object;
                empleado.Area = new ML.Area();
                empleado.Area.Areas = resultArea.Objects.ToList();
                return View(empleado);
            }
            else
            {
                ML.Result result = BL.Empleado.GetById(IdEmpleado.Value);
                if (result.Correct)
                {
                    empleado = (ML.Empleado)result.Object;
                    empleado.Area.Areas = resultArea.Objects.ToList();
                    return View(empleado);
                }
                else
                {
                    ViewBag.Message = result.ErrorMessage;
                    return View();
                }
            }
        }

        [Authorize(Roles = "Administrador")]
        [HttpPost]

        public ActionResult Form(ML.Empleado empleado)
        {
            IFormFile file = Request.Form.Files["IFImage"];

            if (file != null)
            {

                byte[] ImagenBytes = ConvertToBytes(file);

                empleado.Imagen = Convert.ToBase64String(ImagenBytes);
            }

            if (empleado.IdEmpleado == null|| empleado.IdEmpleado == 0)
            {

                ML.Result result = BL.Empleado.Add(empleado);
                if (result.Correct)
                {
                    ViewBag.Message = "Se agrego correctamente el empleado";
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
                ML.Result result = BL.Empleado.Update(empleado);
                if (result.Correct)
                {
                    ViewBag.Message = "Se actualizo correctamente el empleado";
                    return PartialView("Modal");
                }
                else
                {
                    ViewBag.Message = "No se pudo actualizar el empleado";
                    return PartialView("Modal");

                }
            }

        }
        [Authorize(Roles = "Administrador")]
        [HttpGet]
        public ActionResult Delete(int IdEmpleado)
        {
            ML.Empleado empleado = new ML.Empleado();
            empleado.IdEmpleado = IdEmpleado;
            var result = BL.Empleado.Delete(empleado);

            if (result.Correct)
            {
                ViewBag.Message = "Se elimino correctamente el empleado";

            }
            else
            {
                ViewBag.Message = "ocurrio un problema" + result.ErrorMessage;

            }

            return PartialView("Modal");


        }
        public static byte[] ConvertToBytes(IFormFile imagen)
        {

            using var fileStream = imagen.OpenReadStream();

            byte[] bytes = new byte[fileStream.Length];
            fileStream.Read(bytes, 0, (int)fileStream.Length);

            return bytes;
        }
    }
}
