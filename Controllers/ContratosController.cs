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

    private readonly RepositorioPago RepoPago;

    public ContratosController(){
        RepoContratos = new RepositorioContrato();
        RepoInmueble = new RepositorioInmueble();
        RepoInquilino = new RepositorioInquilino();
        RepoPago = new RepositorioPago();

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


         [HttpGet]
    public IActionResult Disponibles(DateTime? fechaInicio, DateTime? fechaFin)
    {
        if (!fechaInicio.HasValue || !fechaFin.HasValue)
        {
            return View(new List<Inmuebles>());
        }

        var inmueblesDisponibles = RepoContratos.GetInmueblesDisponibles(fechaInicio.Value, fechaFin.Value);
        return View(inmueblesDisponibles);
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
        public ActionResult ContratosVigentes()
        {
            var contratos1 = RepoContratos.ObtenerContratosVigentes();
            return View(contratos1);
        }
        [HttpGet]
        public IActionResult ObtenerInmueblesDisponibles(DateTime fechaInicio, DateTime fechaFin)
        {
            var inmueblesDisponibles = RepoInmueble.GetInmueblesDisponiblesPorFecha(fechaInicio, fechaFin);
         return Json(inmueblesDisponibles);
        }
        [Authorize]
        public ActionResult PagosPorContrato(int ContratoId){
            var pagos = RepoPago.ObtenerPagosPorContrato(ContratoId);
            
            ViewBag.ContratoId = ContratoId;
            return View(pagos);
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
[HttpPost]
public ActionResult CrearContrato(Contratos contratos)
{
    try
    {
       

        int res = RepoContratos.Alta(contratos);
        TempData["Id"] = res;

        if (res > 0)
        {
            return RedirectToAction(nameof(IndexC));
        }
        else
        {
            return View(contratos);
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
                ViewBag.EsRenovacion = false;
            return View(contrato);
        }

        public IActionResult RenovarContrato(int id)
        {
        var contrato = RepoContratos.ObtenerContrato(id); // Obtén el contrato de la base de datos
             ViewBag.Inmuebles = RepoInmueble.GetInmuebles();
                ViewBag.Inquilinos =RepoInquilino.GetInquilinos(); // Obtén la lista de inquilinos
            ViewBag.EsRenovacion = true; // Indica que es una renovación
         return View("EditarContrato", contrato); // Reutiliza la misma vista
        }
       
        // POST: Contratos/Edit/5
        [HttpPost]
        [Authorize]
        public ActionResult EditarContrato(int id, Contratos contratos)
        {
           try
            {
               RepoContratos.Modificar(contratos);
               TempData["Mensaje"] = "El Contrato se actualizo correctamente.";

                return RedirectToAction(nameof(IndexC));
            }
            catch(Exception ex)
            {
                throw;
            }
        }
        [HttpPost]
        [Authorize]
        public ActionResult RenovarContrato(int id, Contratos contratos)
        {
           try
            {
               RepoContratos.Modificar(contratos);
               TempData["Mensaje"] = "El Contrato se Renovo correctamente.";

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
        // Obtener el contrato
        var contrato = RepoContratos.ObtenerContrato(id);

        if (contrato == null)
        {
            TempData["Mensaje"] = "El contrato no fue encontrado.";
            return RedirectToAction(nameof(IndexC));
        }

        // Obtener la lista de pagos realizados
        var pagosRealizados = RepoPago.ObtenerPagosPorContrato(id);
        var montoAlquiler = contrato.Monto;
        // Calcular multa
        var multa = RepoContratos.CalcularMulta(contrato, pagosRealizados);

        // Eliminar el contrato
        RepoContratos.Baja(id);

        // Mostrar mensaje de confirmación
        TempData["Mensaje"] = $"El contrato fue terminado. Multa a pagar: {multa:C}.";
        return RedirectToAction(nameof(IndexC));
         }
         catch (Exception ex)
         {
        // Registrar o manejar el error
        TempData["Error"] = "Ocurrió un error al intentar borrar el contrato. Por favor, inténtelo nuevamente.";
        return RedirectToAction(nameof(IndexC));
    }
        }
    }
}