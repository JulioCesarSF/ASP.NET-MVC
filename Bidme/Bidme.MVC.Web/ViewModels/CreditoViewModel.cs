using Bidme.Dominio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bidme.MVC.Web.ViewModels
{
    public class CreditoViewModel
    {

        #region LISTs
        public ICollection<Credito> Creditos { get; set; }
        #endregion

        #region Table Credito
        public int IdCredito { get; set; }
        public int Total { get; set; }
        public int IdPessoa { get; set; }
        #endregion

        #region Table Transacao
        public int IdTransacao { get; set; }
        public System.DateTime Data { get; set; }
        public decimal Valor { get; set; }
        #endregion

    }
}