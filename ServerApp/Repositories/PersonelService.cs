using Microsoft.EntityFrameworkCore;
using ServerApp.Data;
using ServerApp.Entities;
using System;
namespace ServerApp.Repositories
{
    public class PersonelService : IPersonelService
    {
        private readonly RiraContext _dbContext;
        public PersonelService(RiraContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<Personel>> GetListAsync()
        {
            return await _dbContext.Personels.ToListAsync();
        }
        public async Task<Personel> GetByIdAsync(int Id)
        {
            return await _dbContext.Personels.Where(x => x.Id == Id).FirstOrDefaultAsync();
        }
        public async Task<Personel> AddAsync(Personel pers)
        {
            var result = _dbContext.Personels.Add(pers);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }
        public async Task<Personel> UpdateAsync(Personel pers)
        {
            var result = _dbContext.Personels.Update(pers);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }
        public async Task<bool> DeleteAsync(int Id)
        {
            var filteredData = _dbContext.Personels.Where(x => x.Id == Id).FirstOrDefault();
            var result = _dbContext.Remove(filteredData);
            await _dbContext.SaveChangesAsync();
            return result != null ? true : false;
        }
    }
}
