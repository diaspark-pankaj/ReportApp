using LinqKit;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using YoelProject.DAL.DataModels;

namespace YoelProject.DAL.AbstractRepository
{
    /// <summary>
    /// Implementation of Repository Pattern for Entity Framework
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public abstract class AbstractRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {

        DbContext _context;

        /// <summary>
        /// intialize DBcontext
        /// </summary>
        /// <returns></returns>
        public DbContext GetCurrentContext()
        {
            return new ReportApplicationEntities();
        }

        /// <summary>
        /// Gets the context.
        /// </summary>
        /// <value>The context.</value>
        public DbContext Context
        {
            get
            {
                if (_context == null)
                {
                    _context = GetCurrentContext();
                }
                try
                {
                    ((IObjectContextAdapter)_context).ObjectContext.CommandTimeout = 600;
                }
                catch (Exception ex)
                {
                    throw;
                }
                //return _context ?? (_context = GetCurrentUnitOfWork<EFUnitOfWork>().Context); 
                return _context;
            }
            //get { return _context ?? (_context = GetCurrentUnitOfWork<EFUnitOfWork>().Context); }
        }

        /// <summary>
        /// Gets the query.
        /// </summary>
        /// <returns></returns>
        public IQueryable<TEntity> GetQuery()
        {
            return Context.Set<TEntity>();
        }

        public IQueryable<TEntity> GetQueryWithInclude(string entitiesToBeIncluded)
        {
            return Context.Set<TEntity>().Include(entitiesToBeIncluded);
        
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TEntity> GetAll()
        {
            var entities = GetQuery() as DbSet<TEntity>;
            return entities.ToList();
        }

        /// <summary>
        /// Finds the specified where.
        /// </summary>
        /// <param name="predicate">The where.</param>
        /// <returns></returns>
        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().AsExpandable().Where(predicate).AsEnumerable();
        }

    
        /// <summary>
        /// Firsts the specified where.
        /// </summary>
        /// <param name="predicate">The where.</param>
        /// <returns></returns>
        public TEntity First(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().Where(predicate).FirstOrDefault();
        }

        /// <summary>
        /// object exist or not.
        /// </summary>
        /// <param name="predicate">The where.</param>
        /// <returns></returns>
        public bool Any(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().Any(predicate);
        }

        /// <summary>
        /// Get the entity for specified integer id
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public TEntity GetById(int id)
        {
            return Context.Set<TEntity>().Find(id);
        }

        /// <summary>
        /// Get the entity for specified long id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TEntity GetById(long id)
        {
            return Context.Set<TEntity>().Find(id);
        }

        public IEnumerable<TEntity> GetByReferenceID(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().Where(predicate).AsEnumerable();
        }

        /// <summary>
        /// Get the entity for specified string id
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public TEntity GetById(string id)
        {
            return Context.Set<TEntity>().Find(id);
        }

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void Delete(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);
        }

        /// <summary>
        /// Adds the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void Add(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);
        }

        /// <summary>
        /// Attaches the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void Attach(TEntity entity)
        {
            Context.Set<TEntity>().Attach(entity);
        }

        /// <summary>
        /// Saves the changes.
        /// </summary>
        /// 
        public void SaveChanges()
        {
            try
            {
                Context.SaveChanges();

                //Remove cache of saved object ====================
                if (typeof(ICacheble<TEntity>).IsAssignableFrom(typeof(TEntity)) == true)
                {
                    string cacheKey = typeof(TEntity).FullName;
                    System.Diagnostics.Debug.Print("CachedRepository:Clear GetAll of " + cacheKey);
                    System.Web.HttpRuntime.Cache.Remove(cacheKey);
                }
                //=================================================


            }
            catch (DBConcurrencyException ex)
            {
                Console.WriteLine(ex.InnerException != null ? ex.InnerException.Message : ex.Message);
                throw;
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.InnerException != null ? ex.InnerException.Message : ex.Message);
                throw;
            }
            catch (IndexOutOfRangeException ex)
            {
                Console.WriteLine(ex.InnerException != null ? ex.InnerException.Message : ex.Message);
                throw;
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine(ex.InnerException != null ? ex.InnerException.Message : ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        public virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_context != null)
                {
                    _context.Dispose();
                    _context = null;
                }
            }
        }

        public Hashtable CheckValidation(TEntity entity)
        {
            var validationresult = Context.Entry(entity).GetValidationResult();
            Hashtable hstErrors = new Hashtable();
            if (validationresult != null && validationresult.ValidationErrors.Count > 0)
            {
                foreach (var eror in validationresult.ValidationErrors)
                    hstErrors.Add(eror.PropertyName, eror.ErrorMessage);
            }
            return hstErrors;
        }

        public void Entry(TEntity entity, EntityState entityState)
        {
            switch (entityState)
            {
                case EntityState.Added:
                    Context.Entry(entity).State = EntityState.Added;
                    break;
                case EntityState.Modified:
                    Context.Entry(entity).State = EntityState.Modified;
                    break;
                case EntityState.Deleted:
                    Context.Entry(entity).State = EntityState.Deleted;
                    break;
            }
        }
    }    
}
