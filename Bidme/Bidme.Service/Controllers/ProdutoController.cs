using Bidme.Dominio.Models;
using Bidme.Persistencia.UnitsOfWork;
using Bidme.Service.DTOs;
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
        public ICollection<ProdutoDTO> Get()
        {
            //lista para return
            ICollection<ProdutoDTO> lista = new List<ProdutoDTO>();
            //lista do banco
            var produtos = _unit.ProdutoRepository.Listar();

            foreach (var item in produtos)
            {
                lista.Add(new ProdutoDTO()
                {
                    Id = item.Id,
                    Nome = item.Nome,
                    Descricao = item.Descricao,
                    Valor = item.Valor,
                    Imagem = item.Imagem,
                    IdVendedor = item.IdVendedor
                });
            }

            return lista;
        }

        //GET api/produto/id
        //procurar um produto em especifico
        public ProdutoDTO Get(int id)
        {
            var produto = _unit.ProdutoRepository.BuscarPorId(id);
            var p = new ProdutoDTO()
            {
                Id = produto.Id,
                Nome = produto.Nome,
                Descricao = produto.Descricao,
                Valor = produto.Valor,
                Imagem = produto.Imagem,
                IdVendedor = produto.IdVendedor
            };
            return p;
        }

        //GET api/produto/idUser
        //procurar produtos associacos a um usuario com login
        public ICollection<ProdutoDTO> Get(string idUser)
        {
            //lista para return
            ICollection<ProdutoDTO> lista = new List<ProdutoDTO>();
            var produtos = _unit.ProdutoRepository.BuscarPor(p => p.Pessoa.IdUser == idUser);

            foreach (var item in produtos)
            {
                lista.Add(new ProdutoDTO()
                {
                    Id = item.Id,
                    Nome = item.Nome,
                    Descricao = item.Descricao,
                    Valor = item.Valor,
                    Imagem = item.Imagem                    
                });
            }
            return lista;
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
        public IHttpActionResult Put(int id, ProdutoDTO produtoDTO)
        {
            if (ModelState.IsValid)
            {
                var produto = new Produto()
                {
                    Id = produtoDTO.Id,
                    IdVendedor = produtoDTO.IdVendedor,
                    Nome = produtoDTO.Nome,
                    Descricao = produtoDTO.Descricao,
                    Valor = produtoDTO.Valor,
                    Imagem = produtoDTO.Imagem
                };

                try
                {
                    _unit.ProdutoRepository.Alterar(produto);
                    _unit.Salvar();
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("ERROR", e.InnerException.ToString());
                    return BadRequest(ModelState); 
                }
                
                return Ok(produtoDTO);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        //POST api/produto/ObjetoProduto
        //cadastrar um novo produto
        public IHttpActionResult Post(ProdutoDTO produtoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            } 
            
            if(produtoDTO.IdVendedor == 0)
            {
                ModelState.AddModelError("ERROR", "Faltou o IdVendedor");
                return BadRequest(ModelState);
            }

            var produto = new Produto()
            {
                IdVendedor = produtoDTO.IdVendedor,
                Nome = produtoDTO.Nome,
                Descricao = produtoDTO.Descricao,
                Valor = produtoDTO.Valor,
                Imagem = produtoDTO.Imagem
            };

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
            return Created<ProdutoDTO>(new Uri(uri), produtoDTO);
        }
    }
}