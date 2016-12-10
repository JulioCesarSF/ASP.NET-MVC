using Leilao.Dominio.Models;
using Leilao.MVC.Web.ViewModels;
using Leilao.Persistencia.UnitsOfWork;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Leilao.MVC.Web.Controllers
{
    [Authorize]
    public class PainelController : Controller
    {

        #region FIELDs
        private UnitOfWork _unit = new UnitOfWork();
        #endregion

        #region GETs
        [HttpGet]        
        public ActionResult Painel(string idUser)
        {
            var pessoa = _unit.PessoaRepository.BuscarPor(p => p.IdUser == idUser);
            if (pessoa.Count == 1)
            {
                var p = pessoa.First();
                var model = new PessoaViewModel()
                {
                    Nome = p.Nome,
                    //idUser que será usado para add produtos, compras e vendas
                    IdUser = idUser
                };
                return View(model);
            }            
            return RedirectToAction("Login", "User");
        }
        #endregion
    }
}