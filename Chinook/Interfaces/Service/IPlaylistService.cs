using Chinook.ClientModels;
using Chinook.Models;

namespace Chinook.Interfaces.Service
{
    public interface IPlaylistService
    {
        Task<Playlist?> GetFavouritePlaylistByUser(string userId);

        Task<Playlist> Create(Playlist playlist, string userId);

        Task AddTrackToMyFavorite(PlaylistTrackDto playlistTrack, string userId);

        Task RemoveTrackFromMyFavorite(PlaylistTrackDto playlistTrack, string userId);

        Task AddTrackToPlaylist(long playlistId, long trackId, string userId);

        Task<List<PlaylistDto>> GetAll(string userId);

        Task<PlaylistDto> GetPlaylistByPlaylistAndUserId(long playlistId, string userId);

        Task RemoveTrackFromPlaylist(long playlistId, long trackId);

        Task<PlaylistDto?> GetMyFavouritePlaylist(string userId);
    }
}
