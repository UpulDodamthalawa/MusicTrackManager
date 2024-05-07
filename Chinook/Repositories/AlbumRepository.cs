using Chinook.Interfaces.Repository;
using Chinook.Models;
using Microsoft.EntityFrameworkCore;

namespace Chinook.Repositories
{
    public class AlbumRepository : IAlbumRepository
    {
        private ChinookContext _dbContext;
        public AlbumRepository(ChinookContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Album>> GetAlbumsByArtist(long artistId)
        {
            return await _dbContext.Albums.Where(a => a.ArtistId == artistId).ToListAsync();
        }
    }
}
