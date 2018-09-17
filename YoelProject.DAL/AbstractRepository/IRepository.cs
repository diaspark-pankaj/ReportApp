using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace YoelProject.DAL.AbstractRepository
{
    /// <summary>
    /// Repository Pattern Interface
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        /// <summary>
        /// Gets the query.
        /// </summary>
        /// <returns></returns>
        IQueryable<TEntity> GetQuery();

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        IEnumerable<TEntity> GetAll();

        /// <summary>
        /// Gets the by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        TEntity GetById(int id);

        /// <summary>
        /// Finds the specified where.
        /// </summary>
        /// <param name="where">The where.</param>
        /// <returns></returns>
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> where);

        /// <summary>
        /// Firsts the specified where.
        /// </summary>
        /// <param name="where">The where.</param>
        /// <returns></returns>
        TEntity First(Expression<Func<TEntity, bool>> where);

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Delete(TEntity entity);

        /// <summary>
        /// Adds the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Add(TEntity entity);

        /// <summary>
        /// Attaches the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Attach(TEntity entity);

        /// <summary>
        /// Saves the changes.
        /// </summary>
        void SaveChanges();



    }
}
