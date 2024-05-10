using Chinook.Interfaces.Repository;
using Chinook.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Chinook.Constants;
using System.Linq;

namespace Chinook.Repositories
{
    public class PlaylistRepository : IPlaylistRepository
    {
        private ChinookContext _dbContext;
        
        public PlaylistRepository(ChinookContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Playlist> Create(Playlist playlist, string userId)
        {
            _dbContext.Playlists.Add(playlist);
            await _dbContext.SaveChangesAsync();
            
            return playlist;
        }

        public async Task AssignPlaylistToUser(UserPlaylist userPlaylist)
        {
             _dbContext.UserPlaylists.Add(userPlaylist);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Playlist?> GetFavouritePlaylistByUser(string userId)
        {
            return await _dbContext.Playlists.Include(p => p.UserPlaylists).Where(p => p.Name!.Equals(Constant.MyFavouritePlaylist) && p.UserPlaylists.Any(up => up.UserId == userId)).FirstOrDefaultAsync();
        }

        public async Task<long> GetLatestPlaylistId()
        {
            var latestPlaylist = await _dbContext.Playlists.OrderByDescending(e => e.PlaylistId).FirstOrDefaultAsync();
            return latestPlaylist == null ? 1 : latestPlaylist.PlaylistId;
        }

        public async Task AssignTrackToPlaylist(PlaylistTrack playlistTrack)
        {
            _dbContext.PlaylistTrack.Add(playlistTrack);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Playlist>> GetAll(string userId)
        {
            return await _dbContext.Playlists.Where(x => x.Name != Constant.MyFavouritePlaylist && x.UserPlaylists.Any(up => up.UserId == userId))
                .Include(up => up.UserPlaylists)
                .OrderBy(p => p.Name.ToLower()).ToListAsync();
        }

        public async Task<PlaylistTrack?> GetPlaylistTrackByPlaylistAndTrackId(long playlistId, long trackId)
        {
            return await _dbContext.PlaylistTrack.Where(p => p.PlaylistId == playlistId && p.TrackId == trackId).FirstOrDefaultAsync();
        }

        public async Task<Playlist?> GetPlaylistByPlaylistAndUserId(long playlistId, string userId)
        {
            return await _dbContext.Playlists.Where(p => p.PlaylistId == playlistId && p.UserPlaylists.Any(up => up.UserId == userId))
                .Include(pt => pt.PlaylistTracks)
                .ThenInclude(p => p.Track)
                .ThenInclude(a => a.Album)
                .ThenInclude(a=>a.Artist)
                .Include(up => up.UserPlaylists)
                .ThenInclude(u => u.User)
                .FirstOrDefaultAsync();
        }

        public async Task RemoveTrackFromPlaylist(PlaylistTrack playlistTrack)
        {
            _dbContext.PlaylistTrack.Remove(playlistTrack);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Playlist?> GetMyFavouritePlaylist(string userId, string myFavouritePlaylist)
        {
            var favouritePlaylist = await _dbContext.Playlists
                .Where(p => p.Name == myFavouritePlaylist && p.UserPlaylists.Any(up => up.UserId == userId))
                .FirstOrDefaultAsync();
            if (favouritePlaylist != null)
            {
                return favouritePlaylist;
            }

            return null;
        }

        public async Task<IEnumerable<PlaylistTrack>> GetPlaylistTracksByTrackId(long trackId)
        {
            return await _dbContext.PlaylistTrack.Where(p => p.TrackId == trackId).ToListAsync();
        }

        public async Task UpdatePlaylistTracks(IEnumerable<PlaylistTrack> playlistTracks)
        {
            _dbContext.PlaylistTrack.UpdateRange(playlistTracks);
            await _dbContext.SaveChangesAsync();
        }
    }
}
