using Leilao.Dominio.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Leilao.MVC.Web.ViewModels
{
    public class ProdutoViewModel
    {
        #region LISTS
        public ICollection<Produto> Produtos { get; set; }
        #endregion



        #region FIELDs
        public string IdUser { get; set; }
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Imagem { get; set; }
        public decimal Valor { get; set; }
        public int IdVendedor { get; set; }
        #endregion
    }
}