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
       public IActionResult NuevoPago(int ContratoId)
        {
        var contrato = RepoContratos.ObtenerContrato(ContratoId);
        var pagosRealizados = RepoPagos.ObtenerPagosPorContrato(ContratoId);
        DateTime fechaSiguientePago;

    // Si ya hay pagos realizados, toma la fecha del último pago
        if (pagosRealizados.Any())
        {
        fechaSiguientePago = pagosRealizados.Max(p => p.Fecha_Pago).AddMonths(1);
        }
        else
        {
        // Si no hay pagos, comienza desde la fecha de inicio del contrato
        fechaSiguientePago = contrato.Fecha_Inicio;
        }

    // Validar que la fecha del siguiente pago no exceda la duración del contrato
         if (fechaSiguientePago > contrato.Fecha_Fin)
        {
        
        TempData["Mensaje"]= "No se pueden realizar más pagos. El contrato ya está completamente pagado.";
        return RedirectToAction("IndexC","Contratos", new { ContratoId });
        }

    // Crear el modelo para la vista
        var nuevoPago = new Pagos
        {
        ContratoId = ContratoId,
        Fecha_Pago = fechaSiguientePago,
        Importe = contrato.Monto
            };

            return View("CrearPagoParaContrato", nuevoPago);
        }   

        [Authorize]
        [HttpPost]
        public IActionResult NuevoPago(Pagos pago)
        {
            try
        {
        var contrato = RepoContratos.ObtenerContrato(pago.ContratoId);
        var pagosRealizados = RepoPagos.ObtenerPagosPorContrato(pago.ContratoId);

        // Validar que el pago sea para el mes siguiente al último pago
        var ultimaFechaPago = pagosRealizados.Any() ? pagosRealizados.Max(p => p.Fecha_Pago) : contrato.Fecha_Inicio.AddMonths(-1);
        var fechaEsperada = ultimaFechaPago.AddMonths(1);

        if (pago.Fecha_Pago != fechaEsperada)
        {
            ModelState.AddModelError("", $"El pago debe ser para el mes de {fechaEsperada:MMMM yyyy}.");
            return View("CrearPagoParaContrato", pago);
        }
        if(pago.Fecha_Pago == contrato.Fecha_Inicio){

        }
        // Validar que el pago no exceda la fecha de fin del contrato
        if (pago.Fecha_Pago > contrato.Fecha_Fin)
        {
             TempData["Mensaje"] ="No se pueden realizar pagos fuera de la duración del contrato.";
             ViewBag.Mensaje = TempData["Mensaje"];
            return View("PagosPorContratos", pago);
        }

        // Registrar el pago
        int res = RepoPagos.Alta(pago);

        if (res > 0)
        {
            TempData["Mensaje"] = "El pago se realizó correctamente.";
            ViewBag.Mensaje = TempData["Mensaje"];
            return RedirectToAction("PagosPorContrato","Contratos", new { ContratoId = pago.ContratoId });
        }

        ModelState.AddModelError("", "Error al guardar el pago. Inténtelo nuevamente.");
        return View("CrearPagoParaContrato", pago);
        }
         catch (Exception ex)
        {
        ModelState.AddModelError("", "Error al procesar el pago.");
        return View("CrearPagoParaContrato", pago);
        }
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