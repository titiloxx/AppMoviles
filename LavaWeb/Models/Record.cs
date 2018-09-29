using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication6.Models
{
    public class Record
    {
        public string nombre, fechaC,fechaM, estado, link, id, tipo, ventas,valor,sellerId;
        public int precio, diasC, diasM, profit,ventasTotales, reputacion;
    }
    public class Reputacion
    {
        public string id;
        public int ventasTotales, reputacion;
    }
}