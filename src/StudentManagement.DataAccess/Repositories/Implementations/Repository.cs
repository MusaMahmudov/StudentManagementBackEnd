using Microsoft.EntityFrameworkCore;
using StudentManagement.Core.Entities.Common;
using StudentManagement.DataAccess.Contexts;
using StudentManagement.DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.DataAccess.Repositories.Implementations
{
    public class Repository<T> : IRepository<T> where T : BaseSectionEntity
    {
        private readonly AppDbContext _context;
        public Repository(AppDbContext context)
        {
            _context = context;
        }

        public IQueryable<T> GetAll(params string[]? includes)
        {
            var query = _context.Set<T>().AsQueryable();
            if (includes is not null && includes.Length > 0)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            return query;   
        }

        public IQueryable<T> GetFiltered(Expression<Func<T, bool>> expression,params string[]? includes)
        {
            var query = _context.Set<T>().AsQueryable();
            if(includes is not null && includes.Length > 0)
            {
               foreach(var include in includes)
                {
                    query = query.Include(include);
                }
            }

            return query.Where(expression);

        }

        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> expression, params string[]? includes)
        {
            var query =  _context.Set<T>().AsQueryable();
            if (includes is not null && includes.Length > 0)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }
            return await query.FirstOrDefaultAsync(expression);
        }
        public async Task CreateAsync(T entity)
        {
          await _context.Set<T>().AddAsync(entity);   
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public void DeleteSoft(T entity)
        {
            entity.IsDeleted = true;
        }

       

        public async  Task<bool> IsExistsAsync(Expression<Func<T, bool>> expression)
        {
            return await _context.Set<T>().AnyAsync(expression);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }


        public void Update(T entity)
        {
           _context.Set<T>().Update(entity);
        }
    }
}
