using LojaVeiculos.Database;
using System;
using System.Data.Entity;
using System.Linq;

namespace LojaVeiculos.Repositorio.Base
{
    public abstract class Repositorio<TEntity> : IDisposable, IRepositorio<TEntity> where TEntity : class
    {
        readonly Contexto ctx = new Contexto();

        //Busca todos os registros
        public IQueryable<TEntity> GetAll()
        {
            return ctx.Set<TEntity>();
        }

        //Busca o registro de acordo com os critérios recebidos
        public IQueryable<TEntity> Get(Func<TEntity, bool> predicate)
        {
            return GetAll().Where(predicate).AsQueryable();
        }

        //Busca o registro pelo ID (Key)
        public TEntity Find(params object[] key)
        {
            return ctx.Set<TEntity>().Find(key);
        }

        //Adiciona um ítem no modelo
        public void Adicionar(TEntity obj)
        {
            ctx.Set<TEntity>().Add(obj);
        }

        //Atualiza um item do modelo
        public void Atualizar(TEntity obj)
        {
            ctx.Entry<TEntity>(obj).State = EntityState.Modified;
        }

        //Faz as atualizações no banco de dados
        public void SalvarTodos()
        {
            ctx.SaveChanges();
        }

        //Remove 1 
        public void Excluir(TEntity obj)
        {
            ctx.Set<TEntity>().Remove(obj);
        }

        //Remove 1 ou mais ítens
        public void Excluir(Func<TEntity, bool> predicate)
        {
            ctx.Set<TEntity>()
                .Where(predicate).ToList()
                .ForEach(del => ctx.Set<TEntity>().Remove(del));
        }

        //Dispose
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}