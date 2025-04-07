using MiCrudMVC.Data;
using MiCrudMVC.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MiCrudMVC.Controllers
{
    public class AuthController : Controller
    {
        private readonly AppDbContext _context;

        public AuthController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string nombreUsuario, string contraseña)
        {
            var usuario = _context.Usuarios.FirstOrDefault(u => u.NombreUsuario == nombreUsuario && u.Contraseña == contraseña);

            if (usuario == null)
            {
                ViewBag.Error = "Credenciales inválidas";
                return View();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, usuario.NombreUsuario)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity)
            );

            return RedirectToAction("Index", "Productos");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
    }
}
