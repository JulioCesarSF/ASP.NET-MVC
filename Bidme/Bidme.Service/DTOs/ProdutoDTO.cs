using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bidme.Service.DTOs
{
    public class ProdutoDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Imagem { get; set; }
        public decimal Valor { get; set; }
        public int IdVendedor { get; set; }
    }
}