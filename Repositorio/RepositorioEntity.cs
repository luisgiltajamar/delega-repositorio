using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositorio
{
   public class RepositorioEntity<T>: IRepositorio<T> where T:class
   {

       protected DbContext Context;

       public RepositorioEntity(DbContext ctx)
       {
           Context = ctx;
       }

       protected DbSet<T> DbSet
       {
           get { return Context.Set<T>(); }

       }

       public void Dispose()
       {
          
          if(Context!=null)
              Context.Dispose();
       }

       public List<T> Get()
       {
           return DbSet.ToList();
       }

       public T Get(params object[] pk)
       {
           return DbSet.Find(pk);
       }

       public List<T> Get(Expression<Func<T, bool>> expresion)
       {
           return DbSet.Where(expresion).ToList();
       }

       public T Add(T objeto)
       {
           DbSet.Add(objeto);
           try
           {
               Context.SaveChanges();
           }
           catch (Exception e)
           {
               return null;
           }
           return objeto;
       }

       public int Update(T objeto)
       {
           Context.Entry(objeto).State = EntityState.Modified;
           try
           {
             return  Context.SaveChanges();
           }
           catch (Exception e)
           {
               return 0;
           }
       }

       public int Delete(T objeto)
       {
           Context.Entry(objeto).State = EntityState.Deleted;
           try
           {
               return Context.SaveChanges();
           }
           catch (Exception e)
           {
               return 0;
           }
       }

       public int Delete(params object[] pk)
       {
           var obj = DbSet.Find(pk);
           DbSet.Remove(obj);
           try
           {
               return Context.SaveChanges();
           }
           catch (Exception e)
           {
               return 0;
           }
       }

       public int Delete(Expression<Func<T, bool>> expresion)
       {
           var datos = DbSet.Where(expresion);
           DbSet.RemoveRange(datos);
           try
           {
               return Context.SaveChanges();
           }
           catch (Exception e)
           {
               return 0;
           }
       }
   }
}
