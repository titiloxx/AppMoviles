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

        [HttpGet]
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


        [HttpPost]
        public ActionResult Contacto(Persona p)
        {
            if (!ModelState.IsValid)
            { 
                return Contacto("");
            }
            MailManager m = new MailManager();
            var nombre = ((p.Nombre == "") ? "" : p.Nombre + " ");
            var apellido = ((p.Apellido == "") ? "" : p.Apellido + " ");
            m.MandarEmail(p.EmailDestino, "Tu amigo "+ nombre + "Te ha invitado a ver un producto", "Mira este increible producto que te mandó tu amigo " + nombre+" "+apellido
                 + Environment.NewLine + "Producto: " + p.Producto.nombre
                 + Environment.NewLine + "Precio: " + p.Producto.precio
                 + Environment.NewLine + "Dias desde su creacion: " +p.Producto.diasC
                 + Environment.NewLine + "Respondele a su email: " + p.EmailOrigen
                 + Environment.NewLine + Environment.NewLine 
                 + Environment.NewLine + "Saludos, Lavacheats!");
            return View();
        }


        [HttpGet]
        public ActionResult Buscar(string names, int? dias, int? max, int? min, int? page)
        {
            List<List<Record>> listaTotal = new List<List<Record>>();
            List<Record> lista10 = new List<Record>();
            ViewBag.dias = (dias == null) ? 999 : dias.Value;
            ViewBag.min = (min == null) ? 0 : min.Value;
            ViewBag.max = (max == null) ? 99999 : max.Value;
            page = (page == null) ? 0 : page;
            dias = (dias == null) ? 999 : dias;
            max = (max == null) ? 99999 : max;
            min = (min == null) ? 0 : min;
            ViewBag.csgo = "btn-success";
            ViewBag.fortnite = "btn-success";
            ViewBag.gta = "btn-success";

   
            names =(names == null)?"csgo fortnite gta":names;
            var diasAux = (dias == null) ? 999 : dias.Value;
            var minAux = (min == null) ? 0 : min.Value;
            var maxAux = (min == null) ? 99999 : max.Value;
            Meli m = new Meli(754014355650430, "jR9v7lRr06CfzhSOdppHyrNSMhxYKCKb");
            m.Authorize(CoordinadorMl.cambiarVida(), "https://twitter.com");
            CoordinadorMl cord1 = new CoordinadorMl(m);

            bool csgo = names.ToLower().Contains("csgo");
            bool fortnite = names.ToLower().Contains("fortnite");
            bool gta = names.ToLower().Contains("gta");
            if (page==0)
            {
                ViewBag.atras = "disabled";
                var ta = Task.Factory.StartNew(() => {
                    lista10.AddRange(cord1.buscarListaML("", "cheat csgo", diasAux, maxAux, minAux));
                });

                var te = Task.Factory.StartNew(() => {
                    lista10.AddRange(cord1.buscarListaML("", "cheat fortnite", diasAux, maxAux, minAux));
                });

                var ti = Task.Factory.StartNew(() => {
                    lista10.AddRange(cord1.buscarListaML("cheat gta", names, diasAux, maxAux, minAux));
                });
                ta.Wait();
                te.Wait();
                ti.Wait();
            }
            else
            {
                lista10= Session["lista"] as List<Record>;
            }
           
            var listaFiltrada = lista10.Where(x => (x.nombre.ToLower().Contains("cheat") || x.nombre.ToLower().Contains("hack"))&&
            (x.nombre.ToLower().Contains("fortnite")|| x.nombre.ToLower().Contains("csgo")|| x.nombre.ToLower().Contains("gta"))).ToList();
            listaFiltrada = listaFiltrada.Where(x => x.precio < max.Value && x.precio > min.Value && x.diasC < dias.Value).ToList();
            if (!gta)
            {
                listaFiltrada=listaFiltrada.Where(x=>!x.nombre.ToLower().Contains("gta")).ToList();
                ViewBag.gta = "btn-danger";
            }
            if (!csgo)
            {
                listaFiltrada=listaFiltrada.Where(x => !x.nombre.ToLower().Contains("csgo")).ToList();
                ViewBag.csgo = "btn-danger";
            }
            if (!fortnite)
            {
                listaFiltrada=listaFiltrada.Where(x => !x.nombre.ToLower().Contains("fortnite")).ToList();
                ViewBag.fortnite = "btn-danger";
            }
            ViewBag.cantidadTotal = listaFiltrada.Count();
            ViewBag.pagina = page+1;
            Session["lista"] = listaFiltrada;
            int contador = 1;
            List<Record> listAux = new List<Record>();
            foreach (var item in listaFiltrada)
            {
                if (contador<10)
                {
                    listAux.Add(item);
                   
                }
                else
                {
                    listAux.Add(item);
                    listaTotal.Add(listAux);
                    listAux = new List<Record>();
                    contador = -1;
                }
                contador++;
            }
            if (listAux.Count>0)
            {
                listaTotal.Add(listAux);
            }
            if (listaFiltrada != null && listaFiltrada.Count > 0)
            {
                int cont = (listaFiltrada.Count / 10);
                if (listaFiltrada.Count % 10==0)
                {
                    cont--;
                }
                if (page.Value == cont)
                {
                    ViewBag.siguiente = "disabled";
                }
            }
            try
            {
                return View(listaTotal[page.Value]);
            }
            catch 
            {
                return View(new List<Record>());

            }
        
        }

    }
}