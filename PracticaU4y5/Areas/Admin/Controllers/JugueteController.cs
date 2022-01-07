using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using PracticaU4y5.Areas.Admin.Models;
using PracticaU4y5.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PracticaU4y5.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class JugueteController : Controller
    {
        public juguetesContext Context{ get; }
        public IWebHostEnvironment Host { get; }

      
        public JugueteController(juguetesContext context, IWebHostEnvironment host)
        {
            Context = context;
            Host = host;
        }


        public IActionResult Index()
        {
            var j = Context.Juguetes.OrderBy(x => x.Nombre);
            return View(j);
        }
        [HttpGet]
        public IActionResult Agregar()
        {
            AgregarViewModel vm = new AgregarViewModel();
            vm.Empresas = Context.Empresas.OrderBy(x => x.NombreEmpresa);
            return View(vm);
        }
        [HttpPost]
        public IActionResult Agregar(AgregarViewModel vm, IFormFile Archivo)
        {
            if (string.IsNullOrWhiteSpace(vm.juguete.Nombre))
            {
                ModelState.AddModelError(" ", "Nombre esta vacio");
            }
            if (string.IsNullOrWhiteSpace(vm.juguete.Dato.Descripcion))
            {
                ModelState.AddModelError(" ", "La descripcion esta vacia");
            }
            if (vm.juguete.Id != vm.juguete.Id)
            {
                ModelState.AddModelError(" ", "Error");
            }
            if (Context.Juguetes.Any(x=>x.Nombre==vm.juguete.Nombre))
            {
                ModelState.AddModelError(" ", "Juguete ya incluido");
            }
            else
            {
                if (Archivo.ContentType != "image/jpeg")
                {
                    ModelState.AddModelError("", "Solo permitimos la carga de archivos JPG");
                    return View(vm);
                }

                if (Archivo.Length > 1024 * 1024 * 5)
                {
                    ModelState.AddModelError("", "Solo permitimos archivos JPG menores a 5MB");
                    return View(vm);
                }
                Context.Add(vm.juguete);       
                Context.SaveChanges();
                var path = Host.WebRootPath + "/img/" + vm.juguete.Id + ".jpg";

                FileStream fs = new FileStream(path, FileMode.Create);
                Archivo.CopyTo(fs);
                fs.Close();
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Editar(int id)
        {
            AgregarViewModel vm = new AgregarViewModel();
            var j = Context.Juguetes.FirstOrDefault(x => x.Id == id);
            var d = Context.Datos.FirstOrDefault(x => x.Id == id);
            if (j==null)
            {
                return RedirectToAction("Index");
            }
            vm.juguete = j;
            vm.Dato = d;
            vm.Empresas = Context.Empresas.OrderBy(x => x.NombreEmpresa);
            return View(vm);
        }
        [HttpPost]
        public IActionResult Editar(Juguete j, Dato d, IFormFile Archivo)
        {
            var de = Context.Datos.FirstOrDefault(x => x.Id == d.Id);
            var jug = Context.Juguetes.FirstOrDefault(x => x.Id == j.Id);
            if (Context.Juguetes.Any(x=>x.Nombre==j.Nombre&&x.Id!=j.Id))
            {
                ModelState.AddModelError("", "Raza ya incluida");
            }
            else
            {
                if (Archivo.ContentType != "image/jpeg")
                {
                    ModelState.AddModelError("", "Solo permitimos la carga de archivos JPG");
                    return View(jug);
                }

                if (Archivo.Length > 1024 * 1024 * 5)
                {
                    ModelState.AddModelError("", "Solo permitimos archivos JPG menores a 5MB");
                    return View(jug);
                }
                jug.Nombre = j.Nombre;
                jug.Dato.Descripcion = d.Descripcion;
                jug.Dato.Material = d.Material;
                jug.Dato.Peso = d.Peso;                
                jug.Dato.Altura =d.Altura;

                Context.Update(jug);
                Context.SaveChanges();

                if (Archivo != null)
                {

                    var path = Host.WebRootPath + "/img/" + j.Id + ".jpg";

                    FileStream fs = new FileStream(path, FileMode.Create);
                    Archivo.CopyTo(fs);
                    fs.Close();


                }
                return RedirectToAction("Index");
            }
            return View(jug);
        }
        [HttpGet]
        [Authorize(Roles = "Administrador")]
        public IActionResult Eliminar(int id, int od)
        {
            var j = Context.Juguetes.FirstOrDefault(x => x.Id == id);
            var d = Context.Datos.FirstOrDefault(x => x.Id == od);

            if (j==null)
            {
                return RedirectToAction("Index");
            }
            return View(j);
        }
        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public IActionResult Eliminar(Juguete ju, Dato da)
        {
            var jug = Context.Juguetes.FirstOrDefault(x => x.Id == ju.Id);
            var dar = Context.Datos.FirstOrDefault(x => x.Id == da.Id);
            if (jug==null||dar==null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                Context.Remove(dar);
                Context.Remove(jug);
                Context.SaveChanges();
                var path = Host.WebRootPath + "/img/" + ju.Id + ".jpg";

                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                return RedirectToAction("Index");
            }
            
        }
    }
}
