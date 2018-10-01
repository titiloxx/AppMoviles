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
        [Required]
        public string nombre { get; set; }

        [Display(Name = "Creado hace (dias)")]
        [Required]
        public int diasC { get; set; }

        [Display(Name = "Precio")]
        [Required]
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