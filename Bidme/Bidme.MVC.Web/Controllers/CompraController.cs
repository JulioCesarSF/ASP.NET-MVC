using Bidme.Dominio.Models;
using Bidme.MVC.Web.ViewModels;
using Bidme.Persistencia.UnitsOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace Bidme.MVC.Web.Controllers
{
    [Authorize]
    public class CompraController : Controller
    {        
        #region FIELDs
        private UnitOfWork _unit = new UnitOfWork();
        #endregion

        #region GETs
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Comprar(string idUser)
        {
            if (idUser == null)
            {
                string userId = User.Identity.GetUserId();
                if(userId != null)
                {
                    idUser = userId;
                }
                else
                {
                    //se não estiver logado, mostrar todos os produtos disponiveis para compra que estão válidos
                    //negociacoes filtradas validas
                    var negocios = _unit.NegociacaoRepository.BuscarPor(n => n.ValidadeNegociacao.ValidadeDias > 0);

                    var mV = new NegociacaoViewModel()
                    {
                        Negociacoes = negocios
                    };

                    return View(mV);
                }                
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
            //verificar créditos
            //subtrair crédito do total atual
            //calcular valor de acordo com a quantidade de créditos utilizados
            //inserir valor calcular na negociacao
            //atualizar tabela de creditos
            //atualizar tabela de negociacao (valor)
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