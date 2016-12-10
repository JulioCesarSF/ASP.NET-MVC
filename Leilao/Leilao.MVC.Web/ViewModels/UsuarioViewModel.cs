using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Leilao.MVC.Web.ViewModels
{
    public class UsuarioViewModel
    {
        public UserViewModel User { get; set; }
        public PessoaViewModel Pessoa { get; set; }
    }
}