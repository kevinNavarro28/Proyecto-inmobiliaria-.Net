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

        public InmueblesController(){
            RepoInmueble = new RepositorioInmueble();
            RepoPropietario = new RepositorioPropietario();
        }

        // GET: Inmuebles
        public ActionResult IndexIn()
        {
            var listaIn=RepoInmueble.GetInmuebles();
            return View(listaIn);
        }

        // GET: Inmuebles/Details/5
        public ActionResult DetalleInmueble(int id)
        {
           ViewBag.Propietario = RepoPropietario.GetPropietarios();
           var inmueble = RepoInmueble.ObtenerInmueble(id);

            return View(inmueble);
        }

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

        // POST: Inmuebles/Create
        [HttpPost]
        
        public ActionResult CrearInmueble(Inmuebles inmuebles)
        {
            try
            {
                int res = RepoInmueble.Alta(inmuebles);
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

        // GET: Inmuebles/Edit/5
        public ActionResult EditarInmueble(int id)

        {
            ViewBag.Propietarios = RepoPropietario.GetPropietarios();
            var inmueble = RepoInmueble.ObtenerInmueble(id);
            return View(inmueble);
        }

        // POST: Inmuebles/Edit/5
        [HttpPost]
      
        public ActionResult EditarInmueble(int id, Inmuebles inmueble)
        {
            try
            {
               RepoInmueble.Modificar(inmueble);

                return RedirectToAction(nameof(IndexIn));
            }
            catch(Exception ex)
            {
                throw;
            }
        }   
      
        // GET: Inmuebles/Delete/5
        public ActionResult BorrarInmueble(int id)
        {   
             ViewBag.Propietario = RepoPropietario.GetPropietarios();
            var inmueble = RepoInmueble.ObtenerInmueble(id);
            return View(inmueble);
        }
       
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