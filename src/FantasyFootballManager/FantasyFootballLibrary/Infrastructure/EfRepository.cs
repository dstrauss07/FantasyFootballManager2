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
        protected readonly PlayerDbContext RankingRepository;

        public EfRepository(PlayerDbContext playerDbContext)
        {
            RankingRepository = playerDbContext;
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            try
            {
                return await RankingRepository.Set<T>().FindAsync(id);
            }
            catch
            {
                return null;
            }
        }

        public async Task<IReadOnlyList<T>> ListAllAsync()
        {
            return await RankingRepository.Set<T>().ToListAsync();
        }

        public async Task<T> AddAsync(T entity)
        {
            try
            {
                 RankingRepository.Set<T>().Add(entity);
                await RankingRepository.SaveChangesAsync();

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
            RankingRepository.Entry(entity).State = EntityState.Modified;
            await RankingRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            RankingRepository.Set<T>().Remove(entity);
            await RankingRepository.SaveChangesAsync();
        }


    }
}
