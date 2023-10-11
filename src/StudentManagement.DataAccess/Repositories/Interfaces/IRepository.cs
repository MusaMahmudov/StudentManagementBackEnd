using StudentManagement.Core.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.DataAccess.Repositories.Interfaces
{
    public interface IRepository<T> where T : BaseSectionEntity
    {
        IQueryable<T> GetAll(params string[]? includes);
        IQueryable<T> GetFiltered(Expression<Func<T, bool>> expression, params string[]? includes);
        Task<T> GetSingleAsync(Expression<Func<T,bool>> expression, params string[]? includes);
        Task CreateAsync(T entity);
        void Delete(T entity);
        void DeleteList(List<T> entities);
        void DeleteSoft(T entity);
        void Update(T entity);
        Task<int> SaveChangesAsync();
        Task<bool> IsExistsAsync(Expression<Func<T, bool>> expression);
        void AddList(List<T> entities);

        
    }
}
