using ServerApp.Entities;
namespace ServerApp.Repositories
{
    public interface IPersonelService
    {
        public Task<List<Personel>> GetListAsync();
        public Task<Personel> GetByIdAsync(int Id);
        public Task<Personel> AddAsync(Personel pers);
        public Task<Personel> UpdateAsync(Personel pers);
        public Task<bool> DeleteAsync(int Id);

    }
}
