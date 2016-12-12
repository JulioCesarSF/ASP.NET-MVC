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
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class NegociacaoController : ApiController
    {
        #region FIELDs
        private UnitOfWork _unit = new UnitOfWork();
        #endregion

        //GET api/negociacao
        //retornar todas as negociacoes no site
        public ICollection<NegociacaoDTO> Get()
        {
            //list para return
            ICollection<NegociacaoDTO> lista = new List<NegociacaoDTO>();
            //lista do banco
            var negociacoes = _unit.NegociacaoRepository.Listar();
            CriaLista(lista, negociacoes);
            return lista;
        }
        
        //GET api/negociacao/id
        //retornar negociacao pelo id
        public NegociacaoDTO Get(int id)
        {
            //list para return
            NegociacaoDTO n = new NegociacaoDTO();
            //negociacao do banco
            var negociacao = _unit.NegociacaoRepository.BuscarPorId(id);
            n.Id = negociacao.Id;
            n.Data = negociacao.Data;
            n.Valor = negociacao.Valor;
            n.Tipo = negociacao.Tipo;
            n.IdComprador = negociacao.IdComprador;
            n.IdVendedor = negociacao.IdVendedor;
            n.IdProduto = negociacao.IdProduto;
            return n;
        } 
        
        //Get api/negociacao/ comprar 
        //retornar negociacoes abertas para compra
        //TODO melhorar este método
        public ICollection<NegociacaoDTO> Get(string tipo)
        {
            //list para return
            ICollection<NegociacaoDTO> lista = new List<NegociacaoDTO>();
            ICollection<Negociacao> negociacoes = new List<Negociacao>();            
            if (tipo.Equals("comprar"))
            {
                negociacoes = _unit.NegociacaoRepository.BuscarPor(n => n.IdComprador == null);
                CriaLista(lista, negociacoes);
                return lista;
            }
            return lista;
        }

        //POST api/negociacao/ NegociacaoDTO
        //retornar link da negociacao cadastrada
        public IHttpActionResult Post(NegociacaoDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var produto = _unit.ProdutoRepository.BuscarPorId(dto.IdProduto);

            if(produto.Pessoa.IdUser != dto.IdVendedor)
            {
                ModelState.AddModelError("ERROR", "Vendedor não possui este produto cadastrado");
                return BadRequest(ModelState);
            }

            var n = new Negociacao()
            {
                IdVendedor = dto.IdVendedor,
                IdProduto = produto.Id,
                Data = dto.Data,
                Valor = produto.Valor,
                Status = dto.Status,
                Tipo = dto.Tipo
            };

            try
            {
                _unit.NegociacaoRepository.Cadastrar(n);
                _unit.Salvar();
                dto.Id = n.Id;
            }
            catch (Exception e)
            {
                ModelState.AddModelError("ERROR", e.InnerException.ToString());
                return BadRequest(ModelState);                
            }

            var uri = Url.Link("DefaultApi", new { id = n.Id });
            return Created<NegociacaoDTO>(new Uri(uri), dto);
        }

        #region PRIVATEs
        private static void CriaLista(ICollection<NegociacaoDTO> lista, ICollection<Negociacao> negociacoes)
        {
            foreach (var item in negociacoes)
            {
                lista.Add(new NegociacaoDTO()
                {
                    Id = item.Id,
                    Data = item.Data,
                    Valor = item.Valor,
                    Tipo = item.Tipo,
                    IdComprador = item.IdComprador,
                    IdVendedor = item.IdVendedor,
                    IdProduto = item.IdProduto
                });
            }
        }
        #endregion
    }
}