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
            var pessoa = _unit.PessoaRepository.BuscarPor(p => p.IdUser == User.Identity.GetUserId());         
            
            //Debug.WriteLine("idUser pelo controller: " + idUser);
            return View();
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
    }
}