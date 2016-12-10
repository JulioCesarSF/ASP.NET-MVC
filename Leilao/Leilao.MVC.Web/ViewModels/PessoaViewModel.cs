using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Leilao.MVC.Web.ViewModels
{
    public class PessoaViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Cep { get; set; }
        public int Numero { get; set; }
        public string Complemento { get; set; }
        public System.DateTime DataNascimento { get; set; }
        public string Telefone { get; set; }
        public string IdUser { get; set; }
    }
}