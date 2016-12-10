using Leilao.MVC.Web.ViewModels;
using Leilao.Persistencia.UnitsOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Leilao.Dominio.Models;

namespace Leilao.MVC.Web.Controllers
{
    [Authorize]
    public class VendaController : Controller
    {
        #region FIELDs
        private UnitOfWork _unit = new UnitOfWork();
        #endregion

        #region GETs
        [HttpGet]
        public ActionResult Vender(string idUser)
        {
            if(idUser == null)
            {
                return RedirectToAction("Index", "Painel");
            }
            var vendedor = _unit.PessoaRepository.BuscarPor(p => p.IdUser == idUser).First();            
            var model = new ProdutoViewModel()
            {
                Produtos = ListarProdutos(vendedor.Id)
            };
            return View(model);
        }
        #endregion

        #region POSTs
        [HttpPost]
        public ActionResult Vender(ProdutoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Vender", new { idUser = model.IdUser });
            }

            var usuario = _unit.PessoaRepository.BuscarPor(u=>u.IdUser == model.IdUser).First();
            var produto = new Produto()
            {
                Nome = model.Nome,
                Descricao = model.Descricao,
                Valor = model.Valor,
                IdVendedor = usuario.Id,
                Imagem = model.Imagem                           
            };

            _unit.ProdutoRepository.Cadastrar(produto);
            _unit.Salvar();

            return RedirectToAction("Vender", new { idUser = usuario.IdUser });
        }

        [HttpPost]
        public ActionResult Negociar(ProdutoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Vender", new { idUser = model.IdUser });
            }
            var vendedor = _unit.PessoaRepository.BuscarPor(p => p.IdUser == model.IdUser).First();
            var produto = _unit.ProdutoRepository.BuscarPorId(model.Id);
            var negociacao = new Negociacao()
            {
                IdVendedor = vendedor.IdUser,
                Valor = produto.Valor,
                Status = "Em Aberto",                
                Tipo = 1,
                IdProduto = produto.Id,
                Produto = produto                                        
            };

            _unit.NegociacaoRepository.Cadastrar(negociacao);
            _unit.Salvar();

            return RedirectToAction("Vender", new { idUser = vendedor.IdUser });
        }
        #endregion

        #region PRIVATEs
        private ICollection<Produto> ListarProdutos(int idVendedor)
        {
            return _unit.ProdutoRepository.BuscarPor(p => p.IdVendedor == idVendedor);
        }
        #endregion
    }
}