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

        //GET api/produto/id
        //procurar um produto em especifico
        public Produto Get(int id)
        {
            return _unit.ProdutoRepository.BuscarPorId(id);
        }

        //GET api/produto/idUser
        //procurar produtos associacos a um usuario com login
        public ICollection<Produto> Get(string idUser)
        {
            return _unit.ProdutoRepository.BuscarPor(p => p.Pessoa.IdUser == idUser);
        }
 
        //DELETE api/produto/id
        //deletar um produto em especifico
        public void Delete(int id)
        {
            _unit.ProdutoRepository.Remover(id);
            _unit.Salvar();
        }

        //PUT api/produto/
        //atualizar/alterar um produto
        public IHttpActionResult Put(int id, Produto produto)
        {
            if (ModelState.IsValid)
            {                            
                produto.Id = id;
                _unit.ProdutoRepository.Alterar(produto);
                _unit.Salvar();
                return Ok(produto);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        //POST api/produto/ObjetoProduto
        //cadastrar um novo produto
        public IHttpActionResult Post(Produto produto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            } 
            
            if(produto.IdVendedor == 0)
            {
                ModelState.AddModelError("ERROR", "Faltou o IdVendedor");
                return BadRequest(ModelState);
            }           

            try
            {
                _unit.ProdutoRepository.Cadastrar(produto);
                _unit.Salvar();
            }
            catch (Exception e)
            {
                ModelState.AddModelError("ERROR", e.InnerException.ToString());
                return BadRequest(ModelState);
            }            

            //gerar um link para retornar da api com o produto cadastrado
            var uri = Url.Link("DefaultApi", new { id = produto.Id });
            return Created<Produto>(new Uri(uri), produto);
        }
    }
}