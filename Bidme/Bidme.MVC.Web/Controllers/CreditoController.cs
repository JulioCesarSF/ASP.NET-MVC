using Bidme.MVC.Web.ViewModels;
using Bidme.Persistencia.UnitsOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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