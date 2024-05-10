using Chinook.Interfaces.Repository;
using Chinook.Models;
using Microsoft.EntityFrameworkCore;

namespace Chinook.Repositories
{
    public class ArtistRepository : IArtistRepository
    {
        private ChinookContext _dbContext;
        public ArtistRepository(ChinookContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Artist>> GetAll()
        {
            return await _dbContext.Artists.Include(a => a.Albums).ToListAsync();
        }

        public async Task<Artist?> GetArtistById(long artistId)
        {
            return await _dbContext.Artists.Where(a => a.ArtistId == artistId).FirstOrDefaultAsync();
        }
    }
}
