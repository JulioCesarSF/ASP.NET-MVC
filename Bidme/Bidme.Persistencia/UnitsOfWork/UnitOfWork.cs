using Bidme.Dominio.Models;
using Bidme.Persistencia.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bidme.Persistencia.UnitsOfWork
{
    public class UnitOfWork
    {
        #region FIELDS
        public BidmeContext _context = new BidmeContext();

        private IGenericRepository<Pessoa> _pessoaRepository;
        private IGenericRepository<Produto> _produtoRepository;
        private IGenericRepository<Negociacao> _negociacaoRepository;
        private IGenericRepository<Historico> _historicoRepository;
        #endregion

        #region GETs e SETs
        public IGenericRepository<Historico> HistoricoRepository
        {
            get
            {
                if (_historicoRepository == null)
                {
                    _historicoRepository = new GenericRepository<Historico>(_context);
                }
                return _historicoRepository;
            }
            set { _historicoRepository = value; }
        }

        public IGenericRepository<Negociacao> NegociacaoRepository
        {
            get
            {
                if (_negociacaoRepository == null)
                {
                    _negociacaoRepository = new GenericRepository<Negociacao>(_context);
                }
                return _negociacaoRepository;
            }
            set { _negociacaoRepository = value; }
        }

        public IGenericRepository<Produto> ProdutoRepository
        {
            get
            {
                if (_produtoRepository == null)
                {
                    _produtoRepository = new GenericRepository<Produto>(_context);
                }
                return _produtoRepository;
            }
            set { _produtoRepository = value; }
        }

        public IGenericRepository<Pessoa> PessoaRepository
        {
            get
            { 
                if(_pessoaRepository == null)
                {
                    _pessoaRepository = new GenericRepository<Pessoa>(_context);
                }
                return _pessoaRepository;
            }
            set { _pessoaRepository = value; }
        }
        #endregion

        #region DISPOSE
        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
            }
            GC.SuppressFinalize(this);
        }

        #endregion

        #region SAVE
        public void Salvar()
        {
            _context.SaveChanges();
        }

        #endregion

    }
}
