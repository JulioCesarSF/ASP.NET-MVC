//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Bidme.Dominio.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Credito
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Credito()
        {
            this.Transacao = new HashSet<Transacao>();
        }
    
        public int Id { get; set; }
        public int Total { get; set; }
        public int IdPessoa { get; set; }
    
        public virtual Pessoa Pessoa { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Transacao> Transacao { get; set; }
    }
}
