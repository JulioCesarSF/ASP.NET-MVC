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
    
    public partial class Negociacao
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Negociacao()
        {
            this.Historico = new HashSet<Historico>();
        }
    
        public int Id { get; set; }
        public string IdComprador { get; set; }
        public string IdVendedor { get; set; }
        public decimal Valor { get; set; }
        public string Status { get; set; }
        public int Tipo { get; set; }
        public int IdProduto { get; set; }
        public System.DateTime Data { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Historico> Historico { get; set; }
        public virtual Produto Produto { get; set; }
        public virtual ValidadeNegociacao ValidadeNegociacao { get; set; }
    }
}
