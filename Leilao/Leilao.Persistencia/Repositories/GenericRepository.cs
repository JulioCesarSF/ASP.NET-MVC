﻿using Leilao.Dominio.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Leilao.Persistencia.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        #region FIELDS
        protected LeilaoContext _context;
        protected DbSet<T> _dbSet;
        #endregion

        #region METHODs
        public GenericRepository(LeilaoContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public void Alterar(T entidade)
        {
            _context.Entry(entidade).State = EntityState.Modified;
        }

        public ICollection<T> BuscarPor(Expression<Func<T, bool>> filtro)
        {
            return _dbSet.Where(filtro).ToList();
        }

        public T Buscar(string idUser)
        {
            return _dbSet.Find(idUser);
        }

        public T BuscarPorId(int id)
        {
            return _dbSet.Find(id);
        }

        public void Cadastrar(T entidade)
        {
            _dbSet.Add(entidade);
        }

        public ICollection<T> Listar()
        {
            return _dbSet.ToList();
        }

        public void Remover(int id)
        {
            var entity = BuscarPorId(id);
            _dbSet.Remove(entity);
        }

        public ICollection<T> BuscarPor(Expression<Func<T, bool>> filtro, Expression<Func<T, bool>> filtro2)
        {
            return _dbSet.Where(filtro).Where(filtro2).ToList();
        }

        #endregion
    }
}

