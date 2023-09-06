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
    public class InquilinosController : Controller
    {
        private readonly RepositorioInquilino Repo;

        public InquilinosController(){
            Repo = new RepositorioInquilino();
        }
        // GET: Inquilinos
        [Authorize]
        public IActionResult IndexI()
        {   
            
            var lista = Repo.GetInquilinos();
             ViewBag.Id = TempData["Id"];
             if (TempData.ContainsKey("Id"))
				ViewBag.Id = TempData["Id"];
			if (TempData.ContainsKey("Mensaje"))
				ViewBag.Mensaje = TempData["Mensaje"];
            return View(lista);
        }
        [Authorize]
        // GET: Inquilinos/Details/5
        public IActionResult DetalleInquilino(int id)
        {
            var inquilino = Repo.ObtenerInquilino(id);
            return View(inquilino);
        }
        [Authorize]
        // GET: Inquilinos/Create
        public IActionResult CrearInquilino()
        {
            return View();
        }
        [Authorize]
        // POST: Inquilinos/Create
        [HttpPost]
        
        public IActionResult CrearInquilino(Inquilinos inquilino)
        {
            


            try
            {
                
            int res = Repo.Alta(inquilino);
             TempData["Id"] = res;
            if(res> 0 )
            {
                return RedirectToAction(nameof(IndexI));
            }
            else{
                return View(inquilino);
            }
               

               
            }
            catch(Exception ex)
            {
                return View();
            }
        }
        [Authorize]
        // GET: Inquilinos/Edit/5
        public IActionResult EditarInquilino(int id)
        {
            var inquilino = Repo.ObtenerInquilino(id);
            return View(inquilino);
        }
        [Authorize]
        // POST: Inquilinos/Edit/5
        [HttpPost]
        public IActionResult EditarInquilino(int id, Inquilinos inquilino)
        {
            try
            {
               Repo.Modificar(inquilino);
               TempData["Mensaje"] = "El inquilino se actualizo correctamente.";

                return RedirectToAction(nameof(IndexI));
            }
            catch(Exception ex)
            {
                return View();
            }
        }
        [Authorize(Policy ="Administrador")]
        // GET: Inquilinos/Delete/5
        public IActionResult BorrarInquilino(int id)
        {
            var inquilino = Repo.ObtenerInquilino(id);
            return View(inquilino);
        }
        [Authorize(Policy ="Administrador")]
        // POST: Inquilinos/Delete/5
        [HttpPost]
        public IActionResult BorrarInquilino(int id, IFormCollection collection)
        {
            try
            {
               Repo.Borrar(id);

                return RedirectToAction(nameof(IndexI));
            }
            catch(Exception ex)
            {
                return View();
            }
        }
    }
}