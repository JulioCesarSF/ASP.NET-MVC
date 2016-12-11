using Leilao.Dominio.Models;
using Leilao.MVC.Web.ViewModels;
using Leilao.Persistencia.UnitsOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Leilao.MVC.Web.Controllers
{
    public class CompraController : Controller
    {
        #region FIELDs
        private UnitOfWork _unit = new UnitOfWork();
        #endregion

        #region GETs
        [HttpGet]
        public ActionResult Comprar(string idUser)
        {
            if (idUser == null)
            {
                return RedirectToAction("Index", "Painel");
            }
            ICollection<Negociacao> negociacoes = ListarNegociacoes(idUser);
            var model = new NegociacaoViewModel()
            {
                Negociacoes = negociacoes
            };
            return View(model);
        }

        //método para buscar negociações na tela de compra
        [HttpGet]
        public ActionResult BuscarProduto(string nome, string idUser)
        {            
            if (nome == "")
            {
                var negociacoes = _unit.NegociacaoRepository
                    .BuscarPor(n => n.IdComprador == null, n=>n.IdVendedor != idUser)
                    .Where(n=>n.Tipo == 1)
                    .ToList();
                return PartialView("_tabelaProdutosDisponiveis", negociacoes);
            }                     
            var negociacoesComNome = _unit.NegociacaoRepository.BuscarPor(n => n.Produto.Nome == nome, n => n.IdComprador == null)
                .Where(n=>n.Tipo == 1).Where(n=>n.IdVendedor != idUser).ToList();
            return PartialView("_tabelaProdutosDisponiveis", negociacoesComNome);
        }

        [HttpGet]
        public ActionResult VisualizarProduto(int IdProduto)
        {
            return View();
        }
        #endregion

        #region POSTs
        [HttpPost]
        public ActionResult Comprar(NegociacaoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var negociacao = _unit.NegociacaoRepository.BuscarPorId(model.Id);
            var comprador = _unit.PessoaRepository.BuscarPor(p => p.IdUser == model.IdUser).First();            

            negociacao.IdComprador = comprador.IdUser;
            negociacao.Status = model.Status;
            negociacao.Valor = model.Valor;
            //Data em que foi dada a proposta
            negociacao.Data = DateTime.Now;
            negociacao.Tipo = 1;

            _unit.NegociacaoRepository.Alterar(negociacao);
            _unit.Salvar();

            return RedirectToAction("Index", "Painel", new { idUser = model.IdUser });
        }
        #endregion

        #region PRIVATEs
        private ICollection<Negociacao> ListarNegociacoes(string idUser)
        {
            return _unit.NegociacaoRepository.BuscarPor(n => n.IdVendedor != idUser, n => n.Tipo == 1)
                .Where(n => n.IdComprador == null)
                .ToList();
        }

        #endregion
    }
}