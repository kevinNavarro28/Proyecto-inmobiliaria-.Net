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
    public class InmueblesController : Controller
    {
        private readonly RepositorioInmueble RepoInmueble;
        private readonly RepositorioPropietario RepoPropietario;

        private readonly RepositorioContrato RepoContrato;

        public InmueblesController(){
            RepoInmueble = new RepositorioInmueble();
            RepoPropietario = new RepositorioPropietario();
            RepoContrato = new RepositorioContrato();
        }
        [Authorize]
        // GET: Inmuebles
        public ActionResult IndexIn()
        {
            var listaIn=RepoInmueble.GetInmuebles();
             ViewBag.Id = TempData["Id"];
             if (TempData.ContainsKey("Id"))
				ViewBag.Id = TempData["Id"];
			if (TempData.ContainsKey("Mensaje"))
				ViewBag.Mensaje = TempData["Mensaje"];
            return View(listaIn);
        }
        [Authorize]
        public ActionResult ContratosPorInmueble(int InmuebleId)
        {
            var contratos = RepoContrato.ObtenerContratosPorInmueble(InmuebleId);
            return View(contratos);
            }
        [Authorize]
        // GET: Inmuebles/Details/5
        public ActionResult DetalleInmueble(int id)
        {
           ViewBag.Propietario = RepoPropietario.GetPropietarios();
           var inmueble = RepoInmueble.ObtenerInmueble(id);

            return View(inmueble);
        }
        [Authorize]
        // GET: Inmuebles/Create
        public ActionResult CrearInmueble()

        {   
            try{
                
                ViewBag.Propietarios = RepoPropietario.GetPropietarios();
                return View();

            }
            catch(Exception ex){
                throw;
            }
        
        }

        [Authorize]
        // POST: Inmuebles/Create
        [HttpPost]
        
        public ActionResult CrearInmueble(Inmuebles inmuebles)
        {
            try
            {
                int res = RepoInmueble.Alta(inmuebles);
                 TempData["Id"] =res;
                if(res>0){
                    return RedirectToAction(nameof(IndexIn));
                }
                else{
                    return View();
                }
               
                
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [Authorize]
        public ActionResult ObtenerDisponibles(){

             ViewBag.Propietarios = RepoPropietario.GetPropietarios();
             var res = RepoInmueble.ObtenerDisponibles();
                        
          
            return View(res);
            
        }


        [Authorize]
        // GET: Inmuebles/Edit/5
        public ActionResult EditarInmueble(int id)

        {
            ViewBag.Propietarios = RepoPropietario.GetPropietarios();
            var inmueble = RepoInmueble.ObtenerInmueble(id);
            return View(inmueble);
        }
        [Authorize]
        // POST: Inmuebles/Edit/5
        [HttpPost]
      
        public ActionResult EditarInmueble(int id, Inmuebles inmueble)
        {
            try
            {
               RepoInmueble.Modificar(inmueble);
               TempData["Mensaje"] = "El Inmueble se actualizo correctamente.";

                return RedirectToAction(nameof(IndexIn));
            }
            catch(Exception ex)
            {
                throw;
            }
        }   
        [Authorize(Policy ="Administrador")]
        // GET: Inmuebles/Delete/5
        public ActionResult BorrarInmueble(int id)
        {   
             ViewBag.Propietario = RepoPropietario.GetPropietarios();
            var inmueble = RepoInmueble.ObtenerInmueble(id);
            return View(inmueble);
        }
        [Authorize(Policy ="Administrador")]
        // POST: Inmuebles/Delete/5
        [HttpPost]
       
        public ActionResult BorrarInmueble(int id, IFormCollection collection)
        {
            try
            {
               RepoInmueble.Borrar(id);

                return RedirectToAction(nameof(IndexIn));
            }
            catch(Exception ex)
            {
                return View();
            }
        }
    }
}