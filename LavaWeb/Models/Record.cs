using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication6.Models
{
    public class Record
    {
        [Display(Name = "Producto")]
        public string nombre { get; set; }

        [Display(Name = "Creado hace (dias)")]
        public int diasC { get; set; }

        [Display(Name = "Precio")]
        public int precio { get; set; }

        public string fechaC,fechaM, estado, link, id, tipo, ventas,valor,sellerId;
        public int diasM, profit,ventasTotales, reputacion;
    }
    public class Reputacion
    {
        public string id;
        public int ventasTotales, reputacion;
    }
}