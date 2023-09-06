using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using Inmobiliaria.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Net.Http.Headers;


namespace Inmobiliaria.Controllers
{   
     
    public class UsuariosController : Controller

    {   
        private readonly RepositorioUsuario RepoUsuario;

        private readonly IWebHostEnvironment environment;

        public UsuariosController(IWebHostEnvironment environment){
            this.environment = environment;
            RepoUsuario = new RepositorioUsuario();
            
        }
        
        // GET: Usuarios
        [Authorize(Policy ="Administrador")]
        public ActionResult IndexU()
        {   var usuario = RepoUsuario.GetUsuarios();
           
           if (TempData.ContainsKey("Id"))
				ViewBag.Id = TempData["Id"];
			if (TempData.ContainsKey("Mensaje"))
				ViewBag.Mensaje = TempData["Mensaje"];
            return View(usuario);
        }
       
        
        

        [AllowAnonymous]
       public ActionResult Login(string returnUrl)
		{
			TempData["returnUrl"] = returnUrl;
			return View();
		}
        [HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Login(LoginView login)
		{
			try
			{
				var returnUrl = String.IsNullOrEmpty(TempData["returnUrl"] as string) ? "/Home/index" : TempData["returnUrl"].ToString();
				if (ModelState.IsValid)
				{
					string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
						password: login.Clave,
						salt: System.Text.Encoding.ASCII.GetBytes("password"),
						prf: KeyDerivationPrf.HMACSHA1,
						iterationCount: 1000,
						numBytesRequested: 256 / 8));

					var e = RepoUsuario.ObtenerUsuarioPorEmail(login.Usuario);
					if (e == null || e.Clave != hashed)
					{
						ModelState.AddModelError("", "El Mail o la Clave estan incorrectos");
						TempData["returnUrl"] = returnUrl;
						return View();
					}

					var claims = new List<Claim>
					{
						new Claim(ClaimTypes.Name, e.Email),
						new Claim("FullName", e.Nombre + " " + e.Apellido),
						new Claim(ClaimTypes.Role, e.RolNombre),
					};

					var claimsIdentity = new ClaimsIdentity(
							claims, CookieAuthenticationDefaults.AuthenticationScheme);

					await HttpContext.SignInAsync(
							CookieAuthenticationDefaults.AuthenticationScheme,
							new ClaimsPrincipal(claimsIdentity));
					TempData.Remove("returnUrl");
					return Redirect(returnUrl);
				}
				
				return View("Index");
			}
			catch (Exception ex)
			{
				ModelState.AddModelError("", ex.Message);
				return View();
			}
		}
       
        
        [Authorize]

        public async Task<ActionResult> Logout()
		{
			await HttpContext.SignOutAsync(
					CookieAuthenticationDefaults.AuthenticationScheme);
			return RedirectToAction("Login", "Usuarios");
		}
        [AllowAnonymous]
          public ActionResult Restringido()
        {
            return View();
        }
        [Authorize(Policy ="Administrador")]
        // GET: Usuarios/Details/5
        public ActionResult DetalleUsuario(int id)

        {
            var usuario = RepoUsuario.ObtenerUsuario(id);
            ViewBag.Roles = Usuarios.ObtenerRoles();
            return View(usuario);
        }
        [Authorize(Policy ="Administrador")]
        // GET: Usuarios/Create
        public ActionResult CrearUsuario()

        {
            ViewBag.Roles = Usuarios.ObtenerRoles();
            return View();
        }
        
        
        [Authorize(Policy ="Administrador")]
        // POST:Usuarios/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CrearUsuario(Usuarios usuarios)
        { 
            if (!ModelState.IsValid)
                return View(); 
            try
            {
                
                string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                        password: usuarios.Clave,
                        salt: System.Text.Encoding.ASCII.GetBytes("password"),
                        prf: KeyDerivationPrf.HMACSHA1,
                        iterationCount: 1000,
                        numBytesRequested: 256 / 8));
                usuarios.Clave = hashed;
                //usuarios.Rol = usuarios.RolNombre;
                //usuarios.Rol = User.IsInRole("Administrador") ? usuarios.Rol : (int)enRoles.Empleado;
                
                int res = RepoUsuario.Alta(usuarios);
                Console.WriteLine(usuarios.Id);
                if (usuarios.AvatarFile != null && usuarios.Id >=0)
                {
                    string wwwPath = environment.WebRootPath;
                    string path = Path.Combine(wwwPath, "Uploads");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    
                    string fileName = "foto_" + usuarios.Id + Path.GetExtension(usuarios.AvatarFile.FileName);
                    string pathCompleto = Path.Combine(path, fileName);
                    usuarios.Avatarruta = Path.Combine("/Uploads", fileName);
                    // Esta operación guarda la foto en memoria en el ruta que necesitamos
                    using (FileStream stream = new FileStream(pathCompleto, FileMode.Create))
                    {
                        usuarios.AvatarFile.CopyTo(stream);
                    }
                    RepoUsuario.ModificarP(usuarios);
                }   
                
                TempData["Id"] = usuarios.Id;
                return RedirectToAction(nameof(IndexU));
            }
            catch (Exception ex)
            {
                throw;
            
                
            }
        }

        [Authorize]
        public ActionResult Perfil()
        {
            ViewBag.Roles = Usuarios.ObtenerRoles();
            var usuario = RepoUsuario.ObtenerUsuarioPorEmail(User.Identity.Name);
            if (TempData.ContainsKey("Mensaje")){
                ViewBag.Mensaje = TempData["Mensaje"];
            }
            return View(usuario);
        }

        [Authorize]
        public ActionResult EditarPerfil()
        {
            ViewBag.Roles = Usuarios.ObtenerRoles();
            var usuario = RepoUsuario.ObtenerUsuarioPorEmail(User.Identity.Name);
            return View(usuario);
        }

        // GET: Usuario/Edit/5
        [Authorize(Policy = "Administrador")]
        public ActionResult EditarUsuario(int id)
        {
            ViewBag.Roles = Usuarios.ObtenerRoles();
            var usuario = RepoUsuario.ObtenerUsuario(id);
            return View(usuario);
        }

        // POST: Usuario/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "Administrador")]
        public ActionResult EditarUsuario(int id, Usuarios usuarios)
        {
            var usuario = RepoUsuario.ObtenerUsuarioPorEmail(User.Identity.Name);
            try
            {
                if (usuarios.Clave == null || usuarios.Clave == "")
                {
                    usuarios.Clave = usuario.Clave;
                }
                else
                {
                    string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                            password: usuarios.Clave,
                            salt: System.Text.Encoding.ASCII.GetBytes("password"),
                            prf: KeyDerivationPrf.HMACSHA1,
                            iterationCount: 1000,
                            numBytesRequested: 256 / 8
                        ));
                    usuarios.Clave = hashed;
                    usuarios.Nombre = usuario.Nombre;
                    usuarios.Apellido = usuario.Apellido;
                    usuarios.Email = usuario.Email;
                    usuarios.Avatarruta = usuario.Avatarruta;
               
                }

                if (usuarios.AvatarFile != null)
                {   
                usuarios.Nombre = usuario.Nombre;
                usuarios.Apellido = usuario.Apellido;
                usuarios.Email = usuario.Email;
                    //borramos la imagen anterior
                    if (usuario.Avatarruta != null || usuario.Avatarruta != "")
                    {
                        string wwwPath_delete = environment.WebRootPath;
                        string filePath_delete = wwwPath_delete + usuario.Avatarruta;

                        if (System.IO.File.Exists(filePath_delete) && Path.GetFileName(filePath_delete) != "default.webp")
                        {
                            System.IO.File.Delete(filePath_delete);
                            Console.WriteLine("Archivo eliminado exitosamente");
                        }
                    }

                    string wwwPath = environment.WebRootPath;
                    string path = Path.Combine(wwwPath, "Uploads");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    string fileName = "foto_" + usuarios.Id + Path.GetExtension(usuarios.AvatarFile.FileName);
                    string pathCompleto = Path.Combine(path, fileName);
                    usuarios.Avatarruta = Path.Combine("/Uploads", fileName);
                    using (FileStream stream = new FileStream(pathCompleto, FileMode.Create))
                    {
                        usuarios.AvatarFile.CopyTo(stream);
                    }
                }
                else
                {
                    usuarios.Avatarruta = usuario.Avatarruta;
                }
                usuarios.Rol = usuario.Rol;
                

                RepoUsuario.ModificarP(usuarios);
                ViewBag.Roles = Usuarios.ObtenerRoles();
                TempData["Mensaje"] = "El usuario se actualizo correctamente.";
                return RedirectToAction(nameof(IndexU));
            }
            catch
            {
                return View();
            }
        }

        // POST: Usuario/EditarPerfil/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult EditarPerfil(int id, Usuarios usuarioE)
        {
            var usuario = RepoUsuario.ObtenerUsuarioPorEmail(User.Identity.Name);
           
            
            try
            {
                
                

                if (usuarioE.Clave == null || usuarioE.Clave == "")
                {
                    usuarioE.Clave = usuario.Clave;
                }
                else
                {
                    string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                            password: usuarioE.Clave,
                            salt: System.Text.Encoding.ASCII.GetBytes("password"),
                            prf: KeyDerivationPrf.HMACSHA1,
                            iterationCount: 1000,
                            numBytesRequested: 256 / 8
                        ));
                    usuarioE.Clave = hashed;
                     
                    usuarioE.Nombre = usuario.Nombre;
                    usuarioE.Apellido = usuario.Apellido;
                    usuarioE.Email = usuario.Email;
                    usuarioE.Avatarruta = usuario.Avatarruta;
               

                }


                if (usuarioE.AvatarFile != null)
                {
                    usuarioE.Nombre = usuario.Nombre;
                    usuarioE.Apellido = usuario.Apellido;
                    usuarioE.Email = usuario.Email;
                    //borramos la imagen anterior
                    if (usuario.Avatarruta != null || usuario.Avatarruta != "")
                    {
                        string wwwPath_delete = environment.WebRootPath;
                        string filePath_delete = wwwPath_delete + usuario.Avatarruta;

                        if (System.IO.File.Exists(filePath_delete) && Path.GetFileName(filePath_delete) != "default.webp")
                        {
                            System.IO.File.Delete(filePath_delete);
                            Console.WriteLine("Archivo eliminado exitosamente");
                        }
                    }
                    string wwwPath = environment.WebRootPath;
                    string path = Path.Combine(wwwPath, "Uploads");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    string fileName = "foto_" + usuarioE.Id + Path.GetExtension(usuarioE.AvatarFile.FileName);
                    string pathCompleto = Path.Combine(path, fileName);
                    usuarioE.Avatarruta = Path.Combine("/Uploads", fileName);
                    using (FileStream stream = new FileStream(pathCompleto, FileMode.Create))
                    {
                        usuarioE.AvatarFile.CopyTo(stream);
                    }
                }
                else
                {
                    usuarioE.Avatarruta = usuario.Avatarruta;
                }
                usuarioE.Rol = usuario.Rol;
                usuarioE.Id=usuario.Id;
             
                 RepoUsuario.ModificarP(usuarioE);
                ViewBag.Roles = Usuarios.ObtenerRoles();
                TempData["Mensaje"] = "Su perfil ha sido actualizado correctamente.";
                return RedirectToAction(nameof(Index), "Home");
            }   
            catch
            {
                return View();
            }
        }
        
       
        
        
        // GET : Usuarios/Delete/5
        [Authorize(Policy ="Administrador")]
        public ActionResult BorrarUsuario(int id)
        {
            var usuario = RepoUsuario.ObtenerUsuario(id);
            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [Authorize(Policy ="Administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BorrarUsuario(int id, IFormCollection collection)
        {
            try
            {
                
                var usuario = RepoUsuario.ObtenerUsuario(id);

        
                 if (!string.IsNullOrEmpty(usuario.Avatarruta))
                     {
            
                     string wwwPath = environment.WebRootPath;
                     string pathCompleto = Path.Combine(wwwPath, usuario.Avatarruta.TrimStart('/'));

            
                    if (System.IO.File.Exists(pathCompleto))
                        {
                            System.IO.File.Delete(pathCompleto);
                        }
                     }
                RepoUsuario.Borrar(id);
                return RedirectToAction(nameof(IndexU));
            }
            catch(Exception ex)
            {
                throw;
            }
        }
    }
}