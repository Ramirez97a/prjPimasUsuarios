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
    
    public partial class Comments
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Comments()
        {
            this.Comments1 = new HashSet<Comments>();
        }
    
        public int ID { get; set; }
        public int IdWall { get; set; }
        public int IdUsers { get; set; }
        public string Content { get; set; }
        public Nullable<int> ParentID { get; set; }
        public System.DateTime Creation { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Comments> Comments1 { get; set; }
        public virtual Comments Comments2 { get; set; }
        public virtual Wall Wall { get; set; }
        public virtual Users Users { get; set; }
    }
}
