using Bidme.Dominio.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Bidme.MVC.Web.ViewModels
{
    public class CreditoViewModel
    {
        #region LISTs
        public ICollection<Credito> Creditos { get; set; }
        public ICollection<Transacao> Transacoes { get; set; }
        #endregion

        #region Compra de créditos
        [Required]
        public string IdUser { get; set; }
        #endregion

        #region Table Credito
        public int IdCredito { get; set; }
        public int Total { get; set; }
        public int IdPessoa { get; set; }
        #endregion

        #region Table Transacao
        public int IdTransacao { get; set; }
        [Required]
        public System.DateTime Data { get; set; }
        [Required]
        [Display(Name = "Valor R$")]
        public decimal Valor { get; set; }
        #endregion

    }
}