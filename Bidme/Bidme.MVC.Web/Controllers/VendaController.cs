using Bidme.MVC.Web.ViewModels;
using Bidme.Persistencia.UnitsOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bidme.Dominio.Models;
using Microsoft.AspNet.Identity;

namespace Bidme.MVC.Web.Controllers
{
    [Authorize]
    public class VendaController : Controller
    {
        #region FIELDs
        private UnitOfWork _unit = new UnitOfWork();
        #endregion

        #region GETs
        [HttpGet]
        public ActionResult Vender(string idUser, string tipoMensagem, string mensagem)
        {
            if(idUser == null)
            {
                return RedirectToAction("Index", "Painel");
            }
            var vendedor = _unit.PessoaRepository.BuscarPor(p => p.IdUser == idUser).First();            
            var model = new ProdutoViewModel()
            {
                Produtos = ListarProdutos(vendedor.Id),
                TipoMensagem = tipoMensagem,
                Mensagem = mensagem                
            };
            return View(model);
        }
        #endregion

        #region POSTs
        //método responsável por cadastrar o produto para vender
        //TODO:alterar o nome para CadastrarProduto
        [HttpPost]
        public ActionResult CadastrarProduto(ProdutoViewModel model)
        {            
            var vendedor = _unit.PessoaRepository.BuscarPor(p => p.IdUser == model.IdUser).First();
            if (!ModelState.IsValid)
            {
                model.Produtos = ListarProdutos(vendedor.Id);
                return RedirectToAction("Vender", "Venda", 
                    new {
                        idUser = vendedor.IdUser,
                        tipoMensagem = "alert alert-dismissible alert-warning",
                        mensagem = "Dados inválidos",
                    });
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

            try
            {
                _unit.ProdutoRepository.Cadastrar(produto);
                _unit.Salvar();
            }
            catch (Exception e)
            {
                return RedirectToAction("Vender",
                    new
                    {
                        idUser = model.IdUser,
                        tipoMensagem = "alert alert-dismissible alert-warning",
                        mensagem = "Produto NÃO cadastrado. " + e.Message
                    });
            }            

            return RedirectToAction("Vender", 
                new { idUser = usuario.IdUser,
                    tipoMensagem = "alert alert-dismissible alert-success",
                    mensagem = "Produto Cadastrado." });
            }

        //método responsável por inicia uma negociação
        [HttpPost]
        public ActionResult Negociar(NegociacaoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Vender", 
                    new {
                        idUser = model.IdUser,
                    tipoMensagem = "alert alert-dismissible alert-warning",
                    mensagem = "Produto NÃO colocado para venda."
                });
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
                //Data só pode ser o dia atual, em que está iniciando a negociação
                Data = DateTime.Now,
                Produto = produto                                        
            };

            try
            {
                _unit.NegociacaoRepository.Cadastrar(negociacao);
                _unit.Salvar();
            }
            catch (Exception e)
            {
                return RedirectToAction("Vender",
                    new
                    {
                        idUser = model.IdUser,
                        tipoMensagem = "alert alert-dismissible alert-warning",
                        mensagem = "Produto NÃO colocado para venda." + e.Message
                    });
            }

            return RedirectToAction("Vender", new
            {
                idUser = vendedor.IdUser,
                tipoMensagem = "alert alert-dismissible alert-success",
                mensagem = "Produto colocado para venda."
            });
        }

        //método responsável pela tomada de decisão do vendedor em aceitar/recusar
        [HttpPost]
        public ActionResult Proposta(NegociacaoViewModel model)
        {
            if (!ModelState.IsValid || model.Aceite > 2 || model.Aceite < 1)
            {
                return RedirectToAction("Index", "Painel", new { idUser = model.IdUser });
            }

            var negociacao = _unit.NegociacaoRepository.BuscarPorId(model.Id);

            //aceitou
            if(model.Aceite == 1)
            {
                negociacao.Status = "Aguardando Pagamento";
                negociacao.Tipo = 1;

                _unit.NegociacaoRepository.Alterar(negociacao);
                _unit.Salvar(); 

                return RedirectToAction("Index", "Painel", new { idUser = negociacao.IdVendedor });

            }
            //recusou
            else if(model.Aceite == 2)
            {
                negociacao.Status = "Recusado";
                negociacao.Tipo = 2;

                _unit.NegociacaoRepository.Alterar(negociacao);
                _unit.Salvar();

                var historico = new Historico()
                {
                    IdNegociacao = negociacao.Id
                };

                _unit.HistoricoRepository.Cadastrar(historico);
                _unit.Salvar();

                return RedirectToAction("Index", "Painel", new { idUser = negociacao.IdVendedor });
            }
            return View();
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