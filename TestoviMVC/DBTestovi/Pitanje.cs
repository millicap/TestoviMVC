//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TestoviMVC.DBTestovi
{
    using System;
    using System.Collections.Generic;
    
    public partial class Pitanje
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Pitanje()
        {
            this.Odgovors = new HashSet<Odgovor>();
        }
    
        public int PitanjeId { get; set; }
        public int TestId { get; set; }
        public string Tekst { get; set; }
        public short BrojBodova { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Odgovor> Odgovors { get; set; }
        public virtual Test Test { get; set; }
    }
}
