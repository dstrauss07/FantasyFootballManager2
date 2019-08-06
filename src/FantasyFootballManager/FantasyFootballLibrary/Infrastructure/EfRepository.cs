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
        protected readonly PlayerDbContext selectedRepository;

        public EfRepository(PlayerDbContext playerDbContext)
        {
            selectedRepository = playerDbContext;
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            try
            {
                return await selectedRepository.Set<T>().FindAsync(id);
            }
            catch
            {
                return null;
            }
        }

        public async Task<IReadOnlyList<T>> ListAllAsync()
        {
            return await selectedRepository.Set<T>().ToListAsync();
        }

        public async Task<T> AddAsync(T entity)
        {
            try
            {
                selectedRepository.Set<T>().Add(entity);
                await selectedRepository.SaveChangesAsync();

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
            selectedRepository.Entry(entity).State = EntityState.Modified;
            await selectedRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            selectedRepository.Set<T>().Remove(entity);
            await selectedRepository.SaveChangesAsync();
        }


    }
}
