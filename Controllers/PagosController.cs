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

    public class PagosController : Controller
    {
        private readonly RepositorioContrato RepoContratos;
        private readonly RepositorioPago RepoPagos;

        public PagosController(){
            RepoContratos = new RepositorioContrato();
            RepoPagos = new RepositorioPago();
        }
        [Authorize]
        // GET: Pagos
        public ActionResult IndexP()
        {
           try{ var listaPagos = RepoPagos.GetPagos();
            ViewBag.Id = TempData["Id"];
             if (TempData.ContainsKey("Id"))
				ViewBag.Id = TempData["Id"];
			if (TempData.ContainsKey("Mensaje"))
				ViewBag.Mensaje = TempData["Mensaje"];
            
            return View(listaPagos); }
            catch(Exception ex){
                throw;
            }
        }
        [Authorize]
        // GET: Pagos/Details/5
        public ActionResult DetallePago(int Id)
        {   
            var pago = RepoPagos.ObtenerPago(Id);
            return View(pago);
        }
        [Authorize]
        // GET: Pagos/Create
        public ActionResult CrearPago()
        {
            ViewBag.Contrato = RepoContratos.GetContratos();
            return View();
        }
        [Authorize]
        // POST: Pagos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CrearPago(Pagos pagos)
        {
            try
            {
                int res = RepoPagos.Alta(pagos);
                 TempData["Id"] = res;
                if(res>0){
                    return RedirectToAction(nameof(IndexP));
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
        // GET: Pagos/Edit/5
        public ActionResult EditarPago(int Id)
        {
            ViewBag.Contrato = RepoContratos.GetContratos();
            var pago = RepoPagos.ObtenerPago(Id);
            return View(pago);
        }
        [Authorize]
        // POST: Pagos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarPago(int Id,Pagos pagos)
        {
            try
            {
                
                RepoPagos.Modificar(pagos);
                TempData["Mensaje"] = "El Pago se actualizo correctamente.";
                return RedirectToAction(nameof(IndexP));
            }
            catch(Exception ex)
            {
              throw;
            }
        }
        [Authorize(Policy ="Administrador")]
        // GET: Pagos/Delete/5
        public ActionResult BorrarPago(int Id)
        {   var pago = RepoPagos.ObtenerPago(Id);
            return View(pago);
        }
        
        [Authorize(Policy ="Administrador")]
        // POST: Pagos/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BorrarPago(int Id, IFormCollection collection)
        {
            try
            {
               RepoPagos.Borrar(Id);

                return RedirectToAction(nameof(IndexP));
            }
            catch(Exception ex)
            {
                throw;
            }
        }
    }
}