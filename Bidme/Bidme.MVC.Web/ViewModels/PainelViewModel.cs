using Bidme.Dominio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bidme.MVC.Web.ViewModels
{
    public class PainelViewModel
    {
        #region LISTs
        public ICollection<Negociacao> Vendas { get; set; }
        public ICollection<Negociacao> Compras { get; set; }        
        #endregion

        #region FIELDs
        public UsuarioViewModel Usuario { get; set; }
        #endregion
    }
}