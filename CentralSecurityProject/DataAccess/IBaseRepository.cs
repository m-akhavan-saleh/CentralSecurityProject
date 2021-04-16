using System;
using System.Collections.Generic;

namespace CentralSecurityProject.DataAccess
{
    /// <summary>
    /// کلاس پایه ای واسط کاربری جهت پیاده سازی الگوی
    /// Repository Pattern
    /// برای انجام عملیات ثبت ، ویرایش ، حذف و بازیابی اطلاعات
    /// </summary>
    /// <typeparam name="TEntity">موجودیت</typeparam>
    public interface IBaseRepository<TEntity> : IDisposable
        where TEntity : CentralSecurityProject.Models.Security.SecurityBaseModel
    {
        IEnumerable<TEntity> GetAll();
        TEntity GetByID(int Id);
        void Insert(TEntity instance);
        void Delete(int Id);
        void Update(TEntity instance);
        void SaveChanges();
    }
}