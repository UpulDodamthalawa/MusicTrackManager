using Chinook.Interfaces.Repository;
using Chinook.Models;
using Microsoft.EntityFrameworkCore;

namespace Chinook.Repositories
{
    public class TrackRepository : ITrackRepository
    {
        private ChinookContext _dbContext;
        public TrackRepository(ChinookContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Track?> GetById(long trackId)
        {
            return await _dbContext.Tracks.Where(t => t.TrackId == trackId)
                .Include(t=>t.Playlists).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Track>> GetTracksByArtistAndUserId(long artistId, string userId)
        {
            return await _dbContext.Tracks.Where(a => a.Album.ArtistId == artistId)
            .Include(up => up.PlaylistTracks)
            .ThenInclude(p=>p.Playlist)
            .ThenInclude(up=>up.UserPlaylists)
            .ToListAsync();
        }
    }
}
