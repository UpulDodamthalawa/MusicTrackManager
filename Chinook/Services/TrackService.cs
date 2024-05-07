using Chinook.ClientModels;
using Chinook.ExceptionHandlers;
using Chinook.Interfaces.Repository;
using Chinook.Interfaces.Service;
using Chinook.Models;
using System.Runtime.InteropServices;

namespace Chinook.Services
{
    public class TrackService : ITrackService
    {
        private readonly IPlaylistService _playlistService;
        private readonly ITrackRepository _trackRepository;

        public TrackService(ITrackRepository trackRepository, IPlaylistService playlistService)
        {
            _trackRepository = trackRepository;
            _playlistService = playlistService;
        }

        public async Task AddTrackToMyFavourite(PlaylistTrackDto playlistTrack, string? userId)
        {
            if (playlistTrack == null) throw new BadRequestException(ExceptionMessages.PlaylistTrackObjectNotFount());
            if (userId == null) throw new BadRequestException(ExceptionMessages.UserIdParameterNotFoundMessage());

            var track = await _trackRepository.GetById(playlistTrack.TrackId);
            if (track == null) new Exception("Track not exists");

            await _playlistService.AddTrackToMyFavorite(playlistTrack, userId);
        }

        public async Task<PlaylistDto> AddTrackToPlaylist(long playlistId, long trackId, string? userId, [Optional] string? newPlaylistName)
        {
            if (userId == null) throw new BadRequestException(ExceptionMessages.UserIdParameterNotFoundMessage());
            if (playlistId < 0) throw new BadRequestException(ExceptionMessages.PlaylistIdParameterNotValidMessage());
            if (trackId <= 0) throw new BadRequestException(ExceptionMessages.TrackIdParameterNotValidMessage());

            var playlist = new Playlist();

            if (!string.IsNullOrEmpty(newPlaylistName))
            {
                playlist = await _playlistService.Create(new Playlist()
                {
                    Name = newPlaylistName,
                }, userId); 
            }

            await _playlistService.AddTrackToPlaylist(playlistId == 0 ? playlist.PlaylistId: playlistId, trackId, userId);
            return new PlaylistDto(playlist);
        }

        public async Task<List<PlaylistTrackDto>?> GetTracksByArtistAndUserId(long artistId, string userId)
        {
            if (artistId <= 0) throw new BadRequestException(ExceptionMessages.ArtistIdParameterNotValidMessage());
            if (userId == null) throw new BadRequestException(ExceptionMessages.UserIdParameterNotFoundMessage());

            var tracks = await _trackRepository.GetTracksByArtistAndUserId(artistId, userId);

            return tracks != null && tracks.Count() > 0 ? tracks.Select(t => new PlaylistTrackDto(t)
            {
                AlbumTitle = (t.Album == null ? "-" : t.Album.Title),
                TrackId = t.TrackId,
                TrackName = t.Name,
                IsFavorite = t.PlaylistTracks.Where(p => p.IsFavorite && p.UserId != null && p.UserId == userId).Any()
            })
            .ToList() : null;
        }

        public async Task RemoveTrackFromMyFavourite(PlaylistTrackDto playlistTrack, string userId)
        {
            if (playlistTrack == null) throw new BadRequestException(ExceptionMessages.PlaylistTrackObjectNotFount());
            if (userId == null) throw new BadRequestException(ExceptionMessages.UserIdParameterNotFoundMessage());

            var track = await _trackRepository.GetById(playlistTrack.TrackId);
            if (track == null) new Exception("Track not exists");

            await _playlistService.RemoveTrackFromMyFavorite(playlistTrack, userId);
        }
    }
}
