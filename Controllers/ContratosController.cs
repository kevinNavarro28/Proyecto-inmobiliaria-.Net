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
    
    
    public class ContratosController : Controller
    {
    private readonly RepositorioContrato RepoContratos;
    private readonly RepositorioInmueble RepoInmueble;
    private readonly RepositorioInquilino RepoInquilino;

    public ContratosController(){
        RepoContratos = new RepositorioContrato();
        RepoInmueble = new RepositorioInmueble();
        RepoInquilino = new RepositorioInquilino();

    }
        [Authorize]
        // GET: Contratos
            public ActionResult IndexC()

        { var listaContratos = RepoContratos.GetContratos();
                ViewBag.Inmuebles = RepoInmueble.GetInmuebles();
                ViewBag.Inquilinos = RepoInquilino.GetInquilinos();
                ViewBag.Id = TempData["Id"];
              if (TempData.ContainsKey("Id"))
				ViewBag.Id = TempData["Id"];
			if (TempData.ContainsKey("Mensaje"))
				ViewBag.Mensaje = TempData["Mensaje"];

            return View(listaContratos);
        }
        [Authorize]
        // GET: Contratos/Details/5
        public ActionResult DetalleContrato(int id)
        {
            var contrato = RepoContratos.ObtenerContrato(id);
            ViewBag.Inmuebles = RepoInmueble.GetInmuebles();
            ViewBag.Inquilinos = RepoInquilino.GetInquilinos();
            return View(contrato);
        }
        [Authorize]
        // GET: Contratos/Create
        public ActionResult CrearContrato()

        {   
            try{
                
                ViewBag.Inmuebles = RepoInmueble.GetInmuebles();
                ViewBag.Inquilinos = RepoInquilino.GetInquilinos();

                return View();

            }
            catch(Exception ex){
                throw;
            }
        
        }
        [Authorize]
        // POST: Contratos/Create
        [HttpPost]
       
        public ActionResult CrearContrato(Contratos contratos)
        {
            try
            {
                int res = RepoContratos.Alta(contratos);
                 TempData["Id"] = res;
                if(res>0){
                    return RedirectToAction(nameof(IndexC));
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
        // GET: Contratos/Edit/5
        public ActionResult EditarContrato(int id)
        {       ViewBag.Inmuebles = RepoInmueble.GetInmuebles();
                ViewBag.Inquilinos =RepoInquilino.GetInquilinos();
                var contrato = RepoContratos.ObtenerContrato(id);

            return View(contrato);
        }
       
        // POST: Contratos/Edit/5
        [HttpPost]
        [Authorize]
        public ActionResult EditarContrato(int id, Contratos contratos)
        {
           try
            {
               RepoContratos.Modificar(contratos);
               TempData["Mensaje"] = "El Inmueble se actualizo correctamente.";

                return RedirectToAction(nameof(IndexC));
            }
            catch(Exception ex)
            {
                throw;
            }
        }
        [Authorize(Policy ="Administrador")]
        // GET: Contratos/Delete/5
        public ActionResult BorrarContrato(int id)
        {   
            ViewBag.Inmuebles = RepoInmueble.GetInmuebles();
            ViewBag.Inquilinos = RepoInquilino.GetInquilinos();
            var contrato = RepoContratos.ObtenerContrato(id);
            return View(contrato);
        }
       
        // POST: Contratos/Delete/5
        [Authorize(Policy ="Administrador")]
        [HttpPost]

        public ActionResult BorrarContrato(int id, IFormCollection collection)
        {
           try
            {
               RepoContratos.Borrar(id);

                return RedirectToAction(nameof(IndexC));
            }
            catch(Exception ex)
            {
                throw;
            }
        }
    }
}