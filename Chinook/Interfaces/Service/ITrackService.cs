using Chinook.ClientModels;
using System.Runtime.InteropServices;

namespace Chinook.Interfaces.Service
{
    public interface ITrackService
    {
        Task<List<PlaylistTrackDto>?> GetTracksByArtistAndUserId(long artistId, string userId);

        Task AddTrackToMyFavourite(PlaylistTrackDto playlistTrack, string? userId);

        Task RemoveTrackFromMyFavourite(PlaylistTrackDto playlistTrack, string userId);

        Task<PlaylistDto> AddTrackToPlaylist(long playlistId, long trackId, string? userId, [Optional] string? newPlaylistName);
    }
}
