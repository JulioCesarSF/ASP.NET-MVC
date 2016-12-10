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
        public ActionResult Index(string idUser)
        {
            var pessoa = _unit.PessoaRepository.BuscarPor(p => p.IdUser == idUser);
            if (pessoa.Count == 1)
            {
                var p = pessoa.First();
                var model = new PainelViewModel()
                {
                    NegociacoesCompra = ListarCompras(),
                    NegociacoesVenda = ListarVendas(),
                    Usuario = new UsuarioViewModel()
                    {
                        Pessoa = new PessoaViewModel()
                        {
                            Nome = p.Nome,
                            IdUser = p.IdUser
                        }
                    }                   
                };
                return View(model);
            }            
            return RedirectToAction("Login", "User");
        }
        #endregion

        #region PRIVATEs
        //negocioes com ID 1 são Vendas em andamento
        private ICollection<Negociacao> ListarVendas()
        {
            return _unit.NegociacaoRepository.BuscarPor(n=>n.Tipo == 1);
        }

        //negociacoes com ID 2 são Compras
        private ICollection<Negociacao> ListarCompras()
        {
            return _unit.NegociacaoRepository.BuscarPor(n => n.Tipo == 2);
        }
        #endregion
    }
}