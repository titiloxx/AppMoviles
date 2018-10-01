using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using WebApplication6.Models;

namespace LavaWeb.Models
{
    public class Persona
    {
        [Required]
        [Display(Name = "Email Origen")]
        public string EmailOrigen{ get; set; }

        [Required]
        [Display(Name = "Email Destino")]
        public string EmailDestino { get; set; }

        
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }


        [Display(Name = "Apellido")]
        public string Apellido { get; set; }

        [Display(Name = "Producto")]
        public Record Producto { get; set; }
    }
}