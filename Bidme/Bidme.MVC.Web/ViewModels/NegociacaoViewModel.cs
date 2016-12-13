using Bidme.Dominio.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Bidme.MVC.Web.ViewModels
{
    public class NegociacaoViewModel
    {
        #region LISTs
        public ICollection<Negociacao> Negociacoes { get; set; }
        #endregion

        #region Credito
        public CreditoViewModel Creditos { get; set; }
        public int Diamantes { get; set; }
        #endregion

        #region Proposta do vendedor
        public int Aceite { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataValidade { get; set; }
        public int ValidadeDias { get; set; }
        #endregion

        #region FIELDs        
        public int Id { get; set; }
        public string IdUser { get; set; }
        public int IdProduto { get; set; }
        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        public decimal Valor { get; set; }
        public string Status { get; set; }
        public Produto Produto { get; set; }
        #endregion
    }
}