using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PracticaU4y5.Models;
using PracticaU4y5.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracticaU4y5.Controllers
{
    public class HomeController : Controller
    {
        public juguetesContext juguetesContext { get; }
        public HomeController(juguetesContext j)
        {
            juguetesContext = j;
        }
        [Route("/")]
        [Route("Juguete/{id?}")]
        public IActionResult Index(char? c)
        {
            JugueteViewModel vm = new JugueteViewModel();
          
           
            vm.juguete = c == null ? juguetesContext.Juguetes.OrderBy(x => x.Nombre) :
                juguetesContext.Juguetes.Where(x => EF.Functions.Like(x.Nombre, c + "%"));
               
            return View(vm);
        }
        [Route ("{id?}/Juguetes")]
        public IActionResult Juguetes (string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {

                id = id.Replace("-", " ");
                var jug = juguetesContext.Juguetes.Include(x => x.Dato).FirstOrDefault(x => x.Nombre == id);

                if (jug == null)
                {
                    return RedirectToAction("Index");
                }
                return View(jug);
            }
            else
            {
                return RedirectToAction("Index");
            }






        }
    }
}
