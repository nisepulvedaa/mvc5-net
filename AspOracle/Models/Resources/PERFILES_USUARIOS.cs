//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AspOracle.Models.Resources
{
    using System;
    using System.Collections.Generic;
    
    public partial class PERFILES_USUARIOS
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PERFILES_USUARIOS()
        {
            this.EMPLEADOS = new HashSet<EMPLEADOS>();
            this.PROVEEDOR = new HashSet<PROVEEDOR>();
            this.USUARIOS = new HashSet<USUARIOS>();
        }
    
        public int ID_PERFIL { get; set; }
        public string NOMBRE_PERFIL { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EMPLEADOS> EMPLEADOS { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PROVEEDOR> PROVEEDOR { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<USUARIOS> USUARIOS { get; set; }
    }
}
