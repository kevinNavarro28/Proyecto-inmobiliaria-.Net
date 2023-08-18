using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using Inmobiliaria.Models;

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
        // GET: Propietarios
        public IActionResult IndexP()
        {   
            var lista = Repo.GetPropietarios();
            return View(lista);
            
        }

        // GET: Propietarios/Details/5
        public IActionResult DetallePropietario(int id)
        {
            var inquilino = Repo.ObtenerPropietario(id);
            return View(inquilino);
        }

        // GET: Propietarios/Create
        public IActionResult CrearPropietario()
        {
            return View();
        }
      

        // POST: Propietarios/Create
        [HttpPost]
       
        public IActionResult CrearPropietario(Propietarios propietario)
        {
           try
            {
                
            int res = Repo.Alta(propietario);
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

        // GET: Propietarios/Edit/5
        public IActionResult EditarPropietario(int id)
        {
            var inquilino = Repo.ObtenerPropietario(id);
            return View(inquilino);
        }

        // POST: Propietarios/Edit/5

        [HttpPost]
        public IActionResult EditarPropietario(int id, Propietarios propietario)
        {
            try
            {
              Repo.Modificar(propietario);

                return RedirectToAction(nameof(IndexP));
            }
            catch
            {
                return View();
            }
        }

       
        // GET: Propietarios/Delete/5
        public IActionResult BorrarPropietario(int id)
        {
          var propietarios = Repo.ObtenerPropietario(id);
            return View(propietarios);
        }
     
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

