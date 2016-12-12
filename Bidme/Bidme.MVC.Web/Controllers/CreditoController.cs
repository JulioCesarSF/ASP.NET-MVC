using Bidme.MVC.Web.ViewModels;
using Bidme.Persistencia.UnitsOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Diagnostics;
using Bidme.Dominio.Models;

namespace Bidme.MVC.Web.Controllers
{
    [Authorize]
    public class CreditoController : Controller
    {
        #region FIELDs
        private UnitOfWork _unit = new UnitOfWork();
        #endregion

        #region GETs
        [HttpGet]
        public ActionResult Resumo(CreditoViewModel model)
        {
            string idUser = User.Identity.GetUserId();
            var pessoa = _unit.PessoaRepository.BuscarPor(p => p.IdUser == idUser).First();
            //lista de histórico de crédito
            var creditos = ListarCreditos(pessoa.Id);
            int totalCredito = 0;
            if (creditos.Count > 0)
            {
                totalCredito = creditos.Last().Total;
            }
            List<Transacao> transacoes = ListarTransacoesDaPessoa(pessoa);
            model.Transacoes = transacoes;
            model.Total = totalCredito;
            model.IdUser = User.Identity.GetUserId();

            return View(model);
        }
        #endregion

        #region POSTs
        [HttpPost]
        public ActionResult ComprarCredito(CreditoViewModel model)
        {
            var comprador = _unit.PessoaRepository.BuscarPor(p => p.IdUser == model.IdUser).First();
            if (!ModelState.IsValid)
            {
                model.Mensagem = "Compra não realizada. Tente novamente!";
                model.TipoMensagem = "alert alert-dismissable alert-danger";
                model.Transacoes = ListarTransacoesDaPessoa(comprador);
                return RedirectToAction("Resumo", "Credito", model);
            }
            //setup transacao de compra de crédito
            var t = new Transacao()
            {
                Data = model.Data,
                Valor = model.Valor
            };

            var creditos = ListarCreditos(comprador.Id);

            int totalDiamantes = 0;
            if(creditos.Count > 0)
            {
                totalDiamantes = creditos.Last().Total;
            }

            var c = new Credito()
            {
                Total = totalDiamantes + (int)t.Valor,
                IdPessoa = comprador.Id
            };

            try
            {
                _unit.TransacaoRepository.Cadastrar(t);
                _unit.Salvar();
                _unit.CreditoRepository.Cadastrar(c);
                _unit.Salvar();
            }
            catch (Exception e)
            {
                model.Mensagem = "Compra não realizada. Tente novamente! " + e.InnerException.ToString();
                model.TipoMensagem = "alert alert-dismissable alert-danger";
                model.Transacoes = ListarTransacoesDaPessoa(comprador);
                return RedirectToAction("Resumo", "Credito", model);
            }

            model.Mensagem = "Compra realizada, boas compras!";
            model.TipoMensagem = "alert alert-dismissable alert-success";
            model.Transacoes = ListarTransacoesDaPessoa(comprador);
            return RedirectToAction("Resumo", "Credito", model);
        }
        #endregion

        #region PRIVATEs
        private ICollection<Credito> ListarCreditos(int id)
        {
            return _unit.CreditoRepository.BuscarPor(c => c.IdPessoa == id);
        }

        private List<Transacao> ListarTransacoesDaPessoa(Pessoa pessoa)
        {
            //listar todas as transações de compra de crédito
            return _unit.TransacaoRepository.Listar()
                .Where(
                t => t.Credito.Where(c => c.IdPessoa == pessoa.Id) != null
                )
                .ToList();
        }
        #endregion
    }
}