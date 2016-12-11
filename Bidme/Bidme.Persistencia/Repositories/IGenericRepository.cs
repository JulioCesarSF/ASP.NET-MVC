using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Bidme.Persistencia.Repositories
{
    public interface IGenericRepository<T>
    {
        void Cadastrar(T entidade);
        void Alterar(T entidade);
        void Remover(int id);
        ICollection<T> Listar();
        T BuscarPorId(int id);
        ICollection<T> BuscarPor(Expression<Func<T, bool>> filtro);
        ICollection<T> BuscarPor(Expression<Func<T, bool>> filtro, Expression<Func<T, bool>> filtro2);
        T Buscar(string idUser);
    }
}
