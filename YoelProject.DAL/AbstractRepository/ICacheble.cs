
using System;
using System.Collections.Generic;
namespace YoelProject.DAL.AbstractRepository
{
    public interface ICacheble<TEntity> : IDisposable where TEntity : class
    {
        //public interface ICacheble<TEntity> : IDisposable where TEntity : class

        /// <summary>
        /// Gets all cached data.
        /// </summary>
        /// <returns></returns>
        IEnumerable<TEntity> GetAllCached();
    }
}
