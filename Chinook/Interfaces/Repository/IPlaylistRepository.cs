using Chinook.Models;

namespace Chinook.Interfaces.Repository
{
    public interface IPlaylistRepository
    {
        Task<Playlist?> GetFavouritePlaylistByUser(string userId);

        Task<Playlist> Create(Playlist playlist, string userId);

        Task<long> GetLatestPlaylistId();

        Task AssignTrackToPlaylist(PlaylistTrack playlistTrack);

        Task<IEnumerable<Playlist>> GetAll(string userId);

        Task<Playlist?> GetPlaylistByPlaylistAndUserId(long playlistId, string userId);

        Task RemoveTrackFromPlaylist(PlaylistTrack playlistTrack);

        Task<Playlist?> GetMyFavouritePlaylist(string userId, string myFavouritePlaylist);

        Task AssignPlaylistToUser(UserPlaylist playlist);

        Task<PlaylistTrack?> GetPlaylistTrackByPlaylistAndTrackId(long playlistId, long trackId);

        Task<IEnumerable<PlaylistTrack>> GetPlaylistTracksByTrackId(long trackId);

        Task UpdatePlaylistTracks(IEnumerable<PlaylistTrack> playlistTracks);
    }
}
