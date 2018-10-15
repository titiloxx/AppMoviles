using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RestSharp;

namespace LavaWeb.Models
{
    public class Secure
    {
        public static void Registrar(string emailx)
        {
             
            using (MlcrewDBEntities1 db = new MlcrewDBEntities1())
            {
                var seguridad = new Seguridad {ip=BuscarIp()};
                var subscripcion = new Subscripcion { };
                db.Usuarios.Add(new Usuarios {email=emailx,Seguridad= seguridad,Subscripcion= subscripcion });
                db.SaveChanges();
            }
        }
        public static string BuscarIp()
        {
            var client = new RestClient("http://checkip.dyndns.org");
            var request = new RestRequest("", Method.GET);
            var response=client.Execute(request);
            string a4 = response.Content.Split(':')[1].Trim().Split('<')[0];
            return a4;
        }
    }
}