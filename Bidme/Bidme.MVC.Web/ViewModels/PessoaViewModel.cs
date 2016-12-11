using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Bidme.MVC.Web.ViewModels
{
    public class PessoaViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Nome")]
        public string Nome { get; set; }
        [Display(Name = "CPF")]
        public string Cpf { get; set; }
        [Display(Name = "CEP")]
        public string Cep { get; set; }
        [Display(Name = "Número")]
        public int Numero { get; set; }
        [Display(Name = "Complemento")]
        public string Complemento { get; set; }
        [Display(Name = "Data de Nascimento")]
        public System.DateTime DataNascimento { get; set; }
        [Display(Name = "Telefone")]
        public string Telefone { get; set; }
        public string IdUser { get; set; }
    }
}