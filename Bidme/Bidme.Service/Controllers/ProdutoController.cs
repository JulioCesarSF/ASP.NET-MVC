using Bidme.Dominio.Models;
using Bidme.Persistencia.UnitsOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Mvc;

namespace Bidme.Service.Controllers
{
    //limitar o acesso no webservice
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ProdutoController : ApiController
    {
        #region FIELDs
        private UnitOfWork _unit = new UnitOfWork();
        #endregion

        //GET api/produto
        //listar os produtos cadastrados
        public ICollection<Produto> Get()
        {
            return _unit.ProdutoRepository.Listar();
        }
    }
}