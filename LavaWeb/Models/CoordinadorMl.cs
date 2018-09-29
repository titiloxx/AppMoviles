using MercadoLibre.SDK;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RestSharp;

namespace WebApplication6.Models
{
    public class CoordinadorMl
    {

        //Declaracion de variables globales

        Meli m = new Meli(754014355650430, "jR9v7lRr06CfzhSOdppHyrNSMhxYKCKb");
        public CoordinadorMl(Meli meli)
        {
            m = meli;
        }



        private string Mejorarfecha(string fecha)
    {
        string[] a = fecha.Split(' ');
        return a[0];
    }

        public static string cambiarVida()
        {
            //https://auth.mercadolibre.com.ar/authorization?response_type=code&client_id=4830113975240558&redirect_uri=https://twitter.com

            const string formk = "b7e1ab6b3c14edf34be1650b83b47ca4582d796ade1ca3db2bf3d3d22be1450fad8ecca45c16f2ff37a96869feaf015005f2fc4b2240c3aa0fc0359196354cad";
            const string cookie = "ghy-061917-4y2hPyTDNxksJ7F9Pw7sueJUsPuwyS-__-67799972-__-1560978549152--RRR_0-RRR_0";
            var client = new RestClient("https://auth.mercadolibre.com.ar")
            {
                UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64; rv:53.0) Gecko/20100101 Firefox/53.0"
            };
            client.CookieContainer = new System.Net.CookieContainer();
            IRestRequest request = new RestRequest("/authorization/grant", Method.GET);
            request.AddCookie("ssid", cookie);
            request.AddParameter("formtk", formk);
            request.AddParameter("response_type", "code");
            request.AddParameter("client_id", "754014355650430");
            request.AddParameter("redirect_uri", "https://twitter.com");
            IRestResponse response = client.Execute(request);
            char[] delimiters = new char[] { '=' };
            string[] parts = response.ResponseUri.Query.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
            string codigo = parts[1];
            return codigo;

        }
        private List<Record> Filtrar(List<Record> lista)
        {
            Dictionary<string, Record> filtrar = new Dictionary<string, Record>();
            foreach (Record item in lista)
            {
                try
                {
                    filtrar.Add(item.id, item);
                }
                catch
                {

                }
            }
            lista.Clear();
            foreach (KeyValuePair<string, Record> item in filtrar)
            {
                lista.Add(item.Value);
            }
            return lista;
        }

        private bool roto(string roto)
        {
            /*string a = roto.ToLower();
            if (checkBox6.Checked)
            {

                if (a.Contains("se traba") || a.Contains("para repuesto") || a.Contains("para repuestos") || a.Contains("se trava") || a.Contains("se vuelve loco") || a.Contains("parpadea") || a.Contains("no le anda") || a.Contains("genérico") || a.Contains("no inicia") || a.Contains("rota") || a.Contains("roto") || a.Contains("astillada") || a.Contains("astillado") || a.Contains("leer") || a.Contains("trizada")
                || a.Contains("quebrada") || a.Contains("quebrado") || a.Contains("detalle") || a.Contains("no prende") || a.Contains("arreglar")
               || a.Contains("arreglo") || a.Contains("leer") || a.Contains("reparar") || a.Contains("replica") || a.Contains("generico") || a.Contains("generica") || a.Contains("No enciende") || a.Contains("No Enciende"))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }*/
            return false;
        }

        private string fechaRelativa(int dias)
        {
            
            return DateTime.Today.AddDays(-dias).Date.ToString().Split(' ')[0];

        }
        private string paginasTotales(string estado, string buscar)
        {
            var p = new Parameter();
            var pp = new Parameter();
            var pe = new Parameter();
            var pf = new Parameter();
            var pg = new Parameter();
            var ps = new List<Parameter>();
            p.Name = "q";
            p.Value =buscar;
            ps.Add(p);
            pp.Name = "sort";
            pp.Value = "price_asc";
            ps.Add(pp);
            pe.Name = "limit";
            pe.Value = 50;
            ps.Add(pe);
            pg.Name = "condition";
            pg.Value = estado;
            ps.Add(pg);
            IRestResponse response = m.Get("/sites/MLA/search", ps);//[JSON].questions.[0].text
            JObject busqueda = JObject.Parse(response.Content);
            return (string)busqueda["paging"]["total"];
        }
        private int diasML(string fechaML)
        {
            try
            {
               
                 DateTime fechaantes = DateTime.Parse(fechaML);
                 DateTime fechaahora = DateTime.Today.Date;
                 int dias = fechaahora.Subtract(fechaantes).Days;
                 return dias;

                // Descomentar esto para sacarlo del servidor, y comentar lo de arriba
                /*
               string[] words = fechaML.Split('/');
               string inversion = words[1] + "/" + words[0] + "/" + words[2];
               DateTime fechaantes = DateTime.Parse(inversion);
               DateTime fechaahora = DateTime.Today.Date;
               int dias = fechaahora.Subtract(fechaantes).Days;
               return dias;
                */
            }
            catch
            {
                return 2;
            }
        }

        public List<Record> buscarListaML(string estado, string buscar,int dias,int max, int min)
        {
            List<Record> lista = new List<Record>();
            string ab = paginasTotales(estado,buscar);
            decimal resto = Convert.ToInt32(ab) / 50;
            for (int i = 0; i <= Convert.ToInt32(resto); i++)
            {
                lista.AddRange(pedazoListaML((i * 50), estado, buscar,dias,max,min));
            }
            return lista;
        }
        public List<Record> pedazoListaML(int offset, string estado,string buscar,int dias, int max, int min)
    {
        int precio = 0;
        string acomulador = "";
        string sellerAcomulador = "";
        List<Record> lista = new List<Record>();
        var p = new Parameter();
        var pp = new Parameter();
        var pe = new Parameter();
        var pf = new Parameter();
        var pg = new Parameter();
        var pj = new Parameter();
            var ps = new List<Parameter>();
            int cont = 0;
            int cont2 = 0;
            int cont3 = 0;
            p.Name = "q";
        p.Value = buscar;
        ps.Add(p);
        pp.Name = "sort";
        pp.Value = "price_asc";
        ps.Add(pp);
        pf.Name = "offset";
        pf.Value = offset;
        ps.Add(pf);
        pe.Name = "limit";
        pe.Value = 50;
        ps.Add(pe);
        pg.Name = "condition";
        pg.Value = estado;
        ps.Add(pg);
            pj.Name = "access_token";
            pj.Value = m.AccessToken;
            ps.Add(pj);
            IRestResponse response = m.Get("/sites/MLA/search", ps);//[JSON].questions.[0].text
        JObject busqueda = JObject.Parse(response.Content);
        JArray resultados = (JArray)busqueda["results"];
        Dictionary<string, Record> productos = new Dictionary<string, Record>();
        List<Reputacion> ligar = new List<Reputacion>();
            foreach (JObject item in resultados)
        {
            try
            {

                Record producto = new Record();
                producto.nombre = item["title"].ToString();
                producto.estado = (string)item["condition"];
                producto.tipo = (string)item["listing_type_id"];
                producto.precio = Convert.ToInt32(item["price"]);
                producto.link = (string)item["permalink"];
                producto.id = (string)item["id"];
                producto.ventas = (string)item["sold_quantity"];
                productos.Add(producto.id, producto);
            }
            catch
            {

            }

        }

        foreach (var item in productos)
        {
            ++cont2;
        }
            #region SacarFecha

            foreach (var items in productos)
        {
            if (cont == 49)
            {
                acomulador = acomulador + items.Key;
                p = new Parameter();
                var es = new Parameter();
                pe = new Parameter();
                ps = new List<Parameter>();
                p.Name = "attributes";
                p.Value = "last_updated,date_created,id,seller_id";
                es.Name = "ids";
                es.Value = acomulador;
                ps.Add(p);
                ps.Add(es);
                response = m.Get("/items", ps);
                var hola = JArray.Parse(response.Content);
                foreach (JObject item in hola)
                {
                    productos[(string)item["id"]].fechaC = Mejorarfecha((string)item["date_created"]);
                    productos[(string)item["id"]].fechaM = Mejorarfecha((string)item["last_updated"]);
                    productos[(string)item["id"]].sellerId = (string)item["seller_id"];
                    }
                acomulador = "";
                cont = 0;
            }
            else if (cont3 == cont2 - 1)
            {
                acomulador = acomulador + items.Key;
                p = new Parameter();
                var es = new Parameter();
                pe = new Parameter();
                ps = new List<Parameter>();
                p.Name = "attributes";
                p.Value = "last_updated,date_created,id,seller_id";
                es.Name = "ids";
                es.Value = acomulador;
                ps.Add(p);
                ps.Add(es);
                response = m.Get("/items", ps);
                var hola = JArray.Parse(response.Content);
                foreach (JObject item in hola)
                {
                    productos[(string)item["id"]].fechaC = Mejorarfecha((string)item["date_created"]);
                    productos[(string)item["id"]].fechaM = Mejorarfecha((string)item["last_updated"]);
                    productos[(string)item["id"]].sellerId = (string)item["seller_id"];
                    }
                acomulador = "";
                cont = 0;
            }
            else
            {
                acomulador = items.Key + "," + acomulador;
                ++cont;
            }
            ++cont3;
        }
            #endregion

            #region SacarReputacion
            cont = 0;
            cont3 = 0;
            foreach (var items in productos)
            {
             
                if (cont == 49)
                {
                    acomulador = acomulador + items.Value.sellerId;
                    p = new Parameter();
                    var es = new Parameter();
                    pe = new Parameter();
                    ps = new List<Parameter>();
                    p.Name = "attributes";
                    p.Value = "seller_reputation,id";
                    es.Name = "ids";
                    es.Value = acomulador;
                    ps.Add(p);
                    ps.Add(es);
                    response = m.Get("/users", ps);
                    var hola = JArray.Parse(response.Content);
                    foreach (JObject item in hola)
                    {
                       Reputacion repu = new Reputacion();
                       repu.reputacion = Convert.ToInt32(Convert.ToDecimal(item["seller_reputation"]["transactions"]["ratings"]["positive"])*100);
                       repu.ventasTotales = Convert.ToInt32(item["seller_reputation"]["transactions"]["completed"]);
                       repu.id = (string)item["id"];
                       ligar.Add(repu);
                    }
                    acomulador = "";
                    cont = 0;
                }
                else if (cont3 == cont2 - 1)
                {
                    acomulador = acomulador + items.Value.sellerId;
                    p = new Parameter();
                    var es = new Parameter();
                    pe = new Parameter();
                    ps = new List<Parameter>();
                    p.Name = "attributes";
                    p.Value = "seller_reputation,id";
                    es.Name = "ids";
                    es.Value = acomulador;
                    ps.Add(p);
                    ps.Add(es);
                    response = m.Get("/users", ps);
                    var hola = JArray.Parse(response.Content);
                    foreach (JObject item in hola)
                    {
                        Reputacion repu = new Reputacion();
                        repu.reputacion = Convert.ToInt32(Convert.ToDecimal(item["seller_reputation"]["transactions"]["ratings"]["positive"]) * 100);
                        repu.ventasTotales = Convert.ToInt32(item["seller_reputation"]["transactions"]["completed"]);
                        repu.id = (string)item["id"];
                        ligar.Add(repu);
                    }
                    acomulador = "";
                    cont = 0;
                }
                else
                {
                    acomulador = items.Value.sellerId + "," + acomulador;
                    ++cont;
                }
                ++cont3;
            }
            foreach (var item in productos)
            {
                foreach (var items in ligar)
                {
                    if(item.Value.sellerId == items.id)
                    {
                        productos[item.Key].ventasTotales = items.ventasTotales;
                        productos[item.Key].reputacion = items.reputacion;
                    }
                }
                
            }
            #endregion  

            foreach (var item in productos)
        {
                item.Value.diasC = diasML(item.Value.fechaC);
                item.Value.diasM = diasML(item.Value.fechaM);
                if (item.Value.tipo == "gold_special")
                {
                    item.Value.profit = Convert.ToInt32(item.Value.precio * 0.89);
                }
                else
                {
                    item.Value.profit = item.Value.precio;
                }
            if (item.Value.precio <= max && item.Value.precio >= min)
            {
                if (!roto((item.Value.nombre)))
                {
                    if (dias > Convert.ToInt32(diasML(item.Value.fechaC)))
                    {
                            item.Value.fechaC = fechaRelativa(item.Value.diasC);
                            item.Value.fechaM = fechaRelativa(item.Value.diasM);
                            lista.Add(item.Value);
                    }
                }
                }
        }
            return Filtrar(lista);
        }
    }
}