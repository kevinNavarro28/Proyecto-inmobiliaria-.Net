using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using Inmobiliaria.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Net.Http.Headers;



namespace inmobiliaria.Controllers
{   
    public class PropietariosController : Controller
    {   
        private readonly RepositorioPropietario Repo;

        public PropietariosController(){
            Repo = new RepositorioPropietario();

        }
        [Authorize]
        // GET: Propietarios
        public IActionResult IndexP()
        {   
            var lista = Repo.GetPropietarios();
             ViewBag.Id = TempData["Id"];
            if (TempData.ContainsKey("Id"))
				ViewBag.Id = TempData["Id"];
			if (TempData.ContainsKey("Mensaje"))
				ViewBag.Mensaje = TempData["Mensaje"];
            return View(lista);
            
        }

        [Authorize]
        // GET: Propietarios/Details/5
        public IActionResult DetallePropietario(int id)
        {
            var inquilino = Repo.ObtenerPropietario(id);
            return View(inquilino);
        }
        [Authorize]
        // GET: Propietarios/Create
        public IActionResult CrearPropietario()
        {
            return View();
        }
      
        [Authorize]
        // POST: Propietarios/Create
        [HttpPost]
       
        public IActionResult CrearPropietario(Propietarios propietario)
        {
           try
            {
                
            int res = Repo.Alta(propietario);
             TempData["Id"] = res;
            if(res> 0 )
            {
                return RedirectToAction(nameof(IndexP));
            }
            else{
                return View(propietario);
            }
               

               
            }
            catch
            {
                return View();
            }
        }
        [Authorize]
        // GET: Propietarios/Edit/5
        public IActionResult EditarPropietario(int id)
        {
            var inquilino = Repo.ObtenerPropietario(id);
            return View(inquilino);
        }

        // POST: Propietarios/Edit/5
        [Authorize]
        [HttpPost]
        public IActionResult EditarPropietario(int id, Propietarios propietario)
        {
            try
            {
              Repo.Modificar(propietario);
              TempData["Mensaje"] = "El Propietario se actualizo correctamente.";

                return RedirectToAction(nameof(IndexP));
            }
            catch
            {
                return View();
            }
        }

        [Authorize(Policy ="Administrador")]
        // GET: Propietarios/Delete/5
        public IActionResult BorrarPropietario(int id)
        {
          var propietarios = Repo.ObtenerPropietario(id);
            return View(propietarios);
        }
        [Authorize(Policy ="Administrador")]
        // POST: Propietarios/Delete/5
        [HttpPost]
        public IActionResult BorrarPropietario(int id, IFormCollection collection)
        {
            try
            {
                Repo.Borrar(id);

                return RedirectToAction(nameof(IndexP));
            }
            catch
            {
                return View();
            }
        }
    }
}

