using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using StraussDa.FantasyFootballLibrary.Interfaces;

namespace StraussDa.FantasyFootballLibrary.Infrastructure
{
    public class EfRepository<T> : IAsyncRepository<T> where T : BaseEntity
    {
        protected readonly PlayerDbContext _dbContext;

        public EfRepository(PlayerDbContext playerDbContext)
        {
            _dbContext = playerDbContext;
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            try
            {
                return await _dbContext.Set<T>().FindAsync(id);
            }
            catch
            {
                return null;
            }
        }

        public async Task<IReadOnlyList<T>> ListAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<T> AddAsync(T entity)
        {
            try
            {
                 _dbContext.Set<T>().Add(entity);
                await _dbContext.SaveChangesAsync();

                return entity;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return entity;
            }
        }

        public async Task UpdateAsync(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }


    }
}
