using Leilao.Dominio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Leilao.MVC.Web.ViewModels
{
    public class PainelViewModel
    {
        #region LISTs
        public ICollection<Negociacao> NegociacoesVenda { get; set; }
        public ICollection<Negociacao> NegociacoesCompra { get; set; }        
        #endregion

        #region FIELDs
        public UsuarioViewModel Usuario { get; set; }
        #endregion
    }
}