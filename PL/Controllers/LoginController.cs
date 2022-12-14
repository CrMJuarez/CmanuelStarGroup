using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
namespace PL.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Login()
        {
            ML.Usuario usuario = new ML.Usuario();
            return View(usuario);
        }
        [HttpPost]
        public async Task<IActionResult> Login(string contrasenia, string nombreusuario)
        {
            if (contrasenia == "" || contrasenia == null || contrasenia.Equals(""))
            {
                ViewBag.Message = "Usuario o contraseña no ingresado.";
                return PartialView("Modal");
            }
            if (nombreusuario == "" || nombreusuario == null || nombreusuario.Equals(""))
            {
                ViewBag.Message = "Usuario o contraseña no ingresado.";
                return PartialView("Modal");
            }
            ML.Result result = BL.Usuario.GetByNombreUsuario(nombreusuario);
            if (result.Correct)



            {
                ML.Usuario usuario = ((ML.Usuario)result.Object);
                if (usuario.Contrasena == contrasenia)
                {
                    if (usuario.Estatus == true)
                    {
                        var Claims = new List<Claim> {
                new Claim(ClaimTypes.Name, usuario.Nombre),
                new Claim("NombreUsuario",usuario.NombreUsuario)

                };

                        Claims.Add(new Claim(ClaimTypes.Role, usuario.Rol.NombreRol));



                        var ClaimsIdentity = new ClaimsIdentity(Claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(ClaimsIdentity));

                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ViewBag.Message = "Este usuario se encuentra inactivo.";
                        return PartialView("Modal");
                    }
                }
                else
                {
                    ViewBag.Message = "La contraseña no coincide, intente de nuevo";
                    return PartialView("Modal");
                }
            }
            else
            {
                ViewBag.Message = "El usuario no existe, intente de nuevo";
                return PartialView("Modal");
            }

        }


        public ActionResult LoginContrasena()
        {
            ML.Usuario usuario = new ML.Usuario();
            return View(usuario);
        }
        


        public async Task<IActionResult> Salir()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Login");
        }


    }
}
