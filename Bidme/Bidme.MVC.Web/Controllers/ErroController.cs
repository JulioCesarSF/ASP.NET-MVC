using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bidme.MVC.Web.Controllers
{
    public class ErroController : Controller
    {
        #region GETS
        [HttpGet]
        public ActionResult Default()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Erro404()
        {
            return View();
        }

        #endregion
    }
}