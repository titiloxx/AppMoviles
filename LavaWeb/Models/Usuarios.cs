//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LavaWeb.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Usuarios
    {
        public int id { get; set; }
        public int idSubscripcion { get; set; }
        public int idSeguridad { get; set; }
        public string email { get; set; }
    
        public virtual Seguridad Seguridad { get; set; }
        public virtual Subscripcion Subscripcion { get; set; }
    }
}
