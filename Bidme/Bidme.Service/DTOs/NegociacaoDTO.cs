using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bidme.Service.DTOs
{
    public class NegociacaoDTO
    {
        public int Id { get; set; }
        public string IdComprador { get; set; }
        public string IdVendedor { get; set; }
        public decimal Valor { get; set; }
        public string Status { get; set; }
        public DateTime? Data { get; set; }
        public int Tipo { get; set; }
        public int IdProduto { get; set; }
    }
}