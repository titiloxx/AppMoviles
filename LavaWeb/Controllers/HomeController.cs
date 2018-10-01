using LavaWeb.Models;
using MercadoLibre.SDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApplication6.Models;

namespace LavaWeb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contacto(string id)
        {
            var m = Session["lista"] as List<Record>;
            try
            {
                return View(new Persona {Producto= m.First(x => x.id == id) });
            }
            catch 
            {

                return View();
            
            }
            
        }
        [HttpGet]
        public ActionResult Buscar(string names, int? dias, int? max, int? min, int? page)
        {
            ViewBag.dias = (dias == null) ? 999 : dias.Value;
            ViewBag.min = (min == null) ? 0 : min.Value;
            ViewBag.max = (min == null) ? 99999 : max.Value;
            names = !(names==null) ? "" : "csgo fortnite gta";
            if (dias == null&&names.Contains("none"))
            {
                return View();
            }

            var diasAux = (dias == null) ? 999 : dias.Value;
            var minAux = (min == null) ? 0 : min.Value;
            var maxAux = (min == null) ? 99999 : max.Value;
            Meli m = new Meli(754014355650430, "jR9v7lRr06CfzhSOdppHyrNSMhxYKCKb");
            m.Authorize(CoordinadorMl.cambiarVida(), "https://twitter.com");
            CoordinadorMl cord1 = new CoordinadorMl(m);
            List<Record> lista1 = new List<Record>();
            if (names.ToLower().Contains("csgo"))
            {
                var t=Task.Factory.StartNew(() => {
                    lista1.AddRange(cord1.buscarListaML("", "cheat csgo", diasAux, maxAux, minAux));
                });
                t.Wait();
            }
            else
            {
                ViewBag.csgo = "disabled";
            }
            if (names.ToLower().Contains("fortnite"))
            {

                var t = Task.Factory.StartNew(() => {
                    lista1.AddRange(cord1.buscarListaML("", "cheat fortnite", diasAux, maxAux, minAux));
                });
                t.Wait();
            }
            else
            {
                ViewBag.fortnite = "disabled";
            }
            if (names.ToLower().Contains("gta"))
            {
                var t = Task.Factory.StartNew(() => {
                    lista1.AddRange(cord1.buscarListaML("cheat gta", names, diasAux, maxAux, minAux));
                });
                t.Wait();
            }
            else
            {
                ViewBag.gta = "disabled";
            }
            var listaFiltrada = lista1.Where(x => (x.nombre.ToLower().Contains("cheat") || x.nombre.ToLower().Contains("hack"))&&
            (x.nombre.ToLower().Contains("fortnite")|| x.nombre.ToLower().Contains("csgo")|| x.nombre.ToLower().Contains("gta"))).ToList();
            Session["lista"] = listaFiltrada;
            return View(listaFiltrada);
        }
    }
}