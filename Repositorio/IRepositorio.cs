using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositorio
{
    public interface IRepositorio<T>:IDisposable
    {
        List<T> Get();
        T Get(params object[] pk);
        List<T> Get(Expression<Func<T, bool>> expresion);
        T Add(T objeto);
        int Update( T objeto);
        int Delete(T objeto);
        int Delete(params object[] pk);
        int Delete(Expression<Func<T, bool>> expresion);


    }
}
