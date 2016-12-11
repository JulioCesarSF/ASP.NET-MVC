using Bidme.Dominio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bidme.MVC.Web.ViewModels
{
    public class UsuarioViewModel
    {
        #region LISTs
        public ICollection<Negociacao> Negociacoes;
        #endregion
        public UserViewModel User { get; set; }
        public PessoaViewModel Pessoa { get; set; }
    }
}