using System;
using System.Linq;

namespace LojaVeiculos.Repositorio.Base
{
    interface IRepositorio<TEntity> where TEntity : class
    {
        IQueryable<TEntity> GetAll();
        IQueryable<TEntity> Get(Func<TEntity, bool> predicate);
        TEntity Find(params object[] key);
        void Adicionar(TEntity obj);
        void Atualizar(TEntity obj);
        void SalvarTodos();
        void Excluir(Func<TEntity, bool> predicate);
    }
}