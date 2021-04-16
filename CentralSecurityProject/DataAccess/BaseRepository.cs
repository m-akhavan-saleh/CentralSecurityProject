using System;
using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;


namespace CentralSecurityProject.DataAccess
{
    /// <summary>
    /// کلاس پایه ای جهت پیاده سازی الگوی
    /// Repository Pattern
    /// برای انجام عملیات ثبت ، ویرایش ، حذف و بازیابی اطلاعات
    /// </summary>
    public class BaseRepository<TEntity> : IBaseRepository<TEntity>, System.IDisposable
        where TEntity : CentralSecurityProject.Models.Security.SecurityBaseModel
    {
        protected Models.ApplicationDbContext _context;

        public BaseRepository(Models.ApplicationDbContext context)
        {
            this._context = context;
        }

        private DbSet<TEntity> _entityCollection;

        protected DbSet<TEntity> EntityCollection // Singletone
        {
            get
            {
                if (_entityCollection == null)
                    _entityCollection = _context.Set<TEntity>();
                return _entityCollection;
            }
        }

        public IEnumerable<TEntity> GetAll()
        {
            return EntityCollection;
        }

        public TEntity GetByID(int Id)
        {
            return EntityCollection.ToList().Where(x => x.ID == Id).FirstOrDefault();
        }

        public void Insert(TEntity entity)
        {
            EntityCollection.Add(entity);
        }

        public void Delete(int Id)
        {
            _context.Entry(EntityCollection.ToList().Where(x => x.ID == Id).FirstOrDefault()).State = EntityState.Deleted;
        }

        public void Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            System.GC.SuppressFinalize(this);
        }
    }
}