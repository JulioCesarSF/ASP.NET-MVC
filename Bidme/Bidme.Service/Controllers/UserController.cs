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
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class UserController : ApiController
    {
        #region FIELDs
        private UnitOfWork _unit = new UnitOfWork();
        #endregion

        //GET api/user/id
        //método para retornar o idUser associado ao id da pessoa
        public IHttpActionResult Get(int id)
        {
            var user = _unit.PessoaRepository.BuscarPorId(id);
            string idUser = user.IdUser;
            return Ok(idUser);
        }

        //GET api/user/idUser
        //método para retornar o id da Pessoa pelo idUser(login)
        public IHttpActionResult Get(string idUser)
        {
            var user = _unit.PessoaRepository.BuscarPor(p => p.IdUser == idUser).First();
            int id = user.Id;
            return Ok(id);
        }
    }
}