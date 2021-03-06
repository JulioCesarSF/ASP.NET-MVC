﻿using Bidme.Dominio.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Bidme.MVC.Web.ViewModels
{
    public class ProdutoViewModel
    {
        #region LISTS
        public ICollection<Produto> Produtos { get; set; }
        #endregion

        #region FIELDs
        public string IdUser { get; set; }
        public int Id { get; set; }
        [Display(Name = "Nome do Produto")]
        [Required]
        public string Nome { get; set; }
        [Display(Name = "Descrição")]
        [Required]
        public string Descricao { get; set; }
        [Display(Name = "Link para Imagem")]
        [Required]
        public string Imagem { get; set; }
        [Display(Name = "Valor do Produto")]
        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        [Required]
        public decimal Valor { get; set; }
        public int IdVendedor { get; set; }
        #endregion

        #region OUTROs
        public string Mensagem { get; set; }
        public string TipoMensagem { get; set; }
        #endregion
    }
}