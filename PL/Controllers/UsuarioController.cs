using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
namespace PL.Controllers
{
    [Authorize]
    public class UsuarioController : Controller
    {
        public ActionResult GetAll()
        {
            ML.Usuario usuario = new ML.Usuario();
            ML.Result result = BL.Usuario.GetAll();
            //ML.Result resultRol = BL.Rol.GetAll();
            // usuario.Rol = new ML.Rol();

            if (result.Correct)
            {
                usuario.Usuarios = result.Objects.ToList();
                usuario.Rol = new ML.Rol();
                //usuario.Rol.Roles = resultRol.Objects.ToList();
                return View(usuario);
            }
            else
                ViewBag.Message = "ocurrio un problema" + result.ErrorMessage;
            return PartialView("Modal");
        }
        [HttpGet]
        public ActionResult Form(int? IdUsuario)
        {
            ML.Usuario usuario = new ML.Usuario();
            ML.Result resultRol = BL.Rol.GetAll();
            usuario.Rol = new ML.Rol();
            if (IdUsuario == null)
            {
                //usuario = (ML.Usuario)result.Object;
                usuario.Rol = new ML.Rol();
                usuario.Rol.Roles = resultRol.Objects.ToList();
                return View(usuario);
            }
            else
            {
                ML.Result result = BL.Usuario.GetById(IdUsuario.Value);
                if (result.Correct)
                {
                    usuario = (ML.Usuario)result.Object;
                    usuario.Rol.Roles = resultRol.Objects.ToList();
                    return View(usuario);
                }
                else
                {
                    ViewBag.Message = result.ErrorMessage;
                    return View();
                }
            }
        }

        [HttpPost]

        public ActionResult Form(ML.Usuario usuario)
        {
            if (usuario.IdUsuario == null)
            {

                ML.Result result = BL.Usuario.Add(usuario);
                if (result.Correct)
                {
                    ViewBag.Message = "Se agrego correctamente el usuario";
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
                ML.Result result = BL.Usuario.Update(usuario);
                if (result.Correct)
                {
                    ViewBag.Message = "Se actualizo correctamente el usuario";
                    return PartialView("Modal");
                }
                else
                {
                    ViewBag.Message = "No se pudo actualizar el usuario";
                    return PartialView("Modal");

                }
            }

        }
        [HttpGet]
        public ActionResult Delete(int IdUsuario)
        {
            ML.Usuario usuario = new ML.Usuario();
            usuario.IdUsuario = IdUsuario;
            var result = BL.Usuario.Delete(usuario);

            if (result.Correct)
            {
                ViewBag.Message = "Se elimino correctamente el usuario";

            }
            else
            {
                ViewBag.Message = "ocurrio un problema" + result.ErrorMessage;

            }

            return PartialView("Modal");


        }

        public JsonResult CambiarEstatus(ML.Usuario usuario)
        {
            
            ML.Result result = BL.Usuario.ChangeEstatus(usuario);

            return Json(result);
        }

        public ActionResult UpdateEstatus(int IdUsuario)
        {
            ML.Result result = BL.Usuario.GetById(IdUsuario);
            if (result.Correct)
            {
                ML.Usuario usuario = new ML.Usuario();
                usuario = ((ML.Usuario)result.Object);
                usuario.Estatus = usuario.Estatus ? false : true;
                ML.Result resultUpdate = BL.Usuario.Update(usuario);
                if (result.Correct)
                {
                    ViewBag.Message = "Se actualizo el Estatus";
                }
                else
                {
                    ViewBag.Message = "Problema al actualizar";
                }

            }
            return PartialView("Modal");
        }

    }
}

