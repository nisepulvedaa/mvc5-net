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
    
    public partial class MENU
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MENU()
        {
            this.DETALLE_COMEDORES = new HashSet<DETALLE_COMEDORES>();
        }
    
        public int ID_MENU { get; set; }
        public string NOMBRE_MENU { get; set; }
        public Nullable<int> TIPO_MENU { get; set; }
        public Nullable<int> ESTILO_MENU { get; set; }
        public string DESCRIPCION_MENU { get; set; }
        public Nullable<int> VALOR_MENU { get; set; }
        public Nullable<int> CANTIDAD_MENU { get; set; }
        public Nullable<int> ESTADO_MENU { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DETALLE_COMEDORES> DETALLE_COMEDORES { get; set; }
    }
}
