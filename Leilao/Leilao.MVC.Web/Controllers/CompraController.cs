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
            var negociacoes = _unit.NegociacaoRepository.BuscarPor(n => n.IdVendedor != idUser);
            var pessoa = _unit.PessoaRepository.BuscarPor(p => p.IdUser == idUser).First();
            var model = new UsuarioViewModel()
            {
                Negociacoes = negociacoes,
                Pessoa = new PessoaViewModel()
                {
                    Nome = pessoa.Nome,
                    IdUser = pessoa.IdUser
                }
            };
            return View(model);
        }
        #endregion

        #region POSTs
        [HttpPost]
        public ActionResult VisualizarProduto(int IdProduto)
        {
            return View();
        }
        #endregion
    }
}