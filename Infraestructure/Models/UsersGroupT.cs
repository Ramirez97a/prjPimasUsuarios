//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Infraestructure.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class UsersGroupT
    {
        public int RoleID { get; set; }
        public int GroupID { get; set; }
        public int UserID { get; set; }
    
        public virtual GroupT GroupT { get; set; }
        public virtual Rol Rol { get; set; }
        public virtual Users Users { get; set; }
    }
}