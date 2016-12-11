using Leilao.Dominio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Leilao.MVC.Web.ViewModels
{
    public class NegociacaoViewModel
    {
        #region LISTs
        public ICollection<Negociacao> Negociacoes { get; set; }
        #endregion

        #region FIELDs
        public int Id { get; set; }
        public string IdUser { get; set; }
        public int IdProduto { get; set; }
        public decimal Valor { get; set; }
        public string Status { get; set; }
        public Produto Produto { get; set; }
        #endregion
    }
}