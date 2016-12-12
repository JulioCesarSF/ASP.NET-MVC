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
        public ActionResult Resumo()
        {
            string idUser = User.Identity.GetUserId();
            var pessoa = _unit.PessoaRepository.BuscarPor(p => p.IdUser == idUser).First();
            //lista de histórico de crédito
            var creditos = ListarCreditos(pessoa.Id);
            int totalCredito = 0;
            if (creditos.Count > 0)
            {
                totalCredito = creditos.First().Total;
            }
            //listar todas as transações de compra de crédito
            var transacoes = _unit.TransacaoRepository.Listar()
                .Where(
                t=>t.Credito.Where(c=>c.IdPessoa == pessoa.Id) != null
                )
                .ToList();
            
            var model = new CreditoViewModel()
            {
                Transacoes = transacoes,
                Total = totalCredito,
                IdUser = User.Identity.GetUserId()            
            };
            return View(model);
        }     

        [HttpGet]
        public ActionResult ComprarCredito()
        {
            return View();
        }
        #endregion

        #region POSTs
        [HttpPost]
        public ActionResult ComprarCredito(CreditoViewModel model)
        {
            return View();
        }
        #endregion

        #region PRIVATEs
        private ICollection<Credito> ListarCreditos(int id)
        {
            return _unit.CreditoRepository.BuscarPor(c => c.IdPessoa == id);
        }
        #endregion
    }
}