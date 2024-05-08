using Chinook.ClientModels;
using Chinook.Constants;
using Chinook.ExceptionHandlers;
using Chinook.Interfaces.Repository;
using Chinook.Interfaces.Service;
using Chinook.Models;

namespace Chinook.Services
{
    public class PlaylistService : IPlaylistService
    {
        private readonly IPlaylistRepository _playlistRepository;
        public PlaylistService(IPlaylistRepository playlistRepository)
        {
            _playlistRepository = playlistRepository;
        }

        public async Task AddTrackToPlaylist(long playlistId, long trackId, string userId)
        {
            var playlistTrack = await _playlistRepository.GetPlaylistTrackByPlaylistAndTrackId(playlistId, trackId);

            if(playlistTrack == null)
            {
                var favoritePlaylist = await _playlistRepository.GetMyFavouritePlaylist(userId, Constant.MyFavouritePlaylist);
                var favoritePlaylistTrack = favoritePlaylist != null ? await _playlistRepository.GetPlaylistTrackByPlaylistAndTrackId(favoritePlaylist.PlaylistId, trackId) : null;

                playlistTrack = new PlaylistTrack
                {
                    TrackId = trackId,
                    PlaylistId=playlistId,
                    IsFavorite = favoritePlaylistTrack != null,
                    UserId = userId

                };

                await _playlistRepository.AssignTrackToPlaylist(playlistTrack);
            }
        }

        public async Task<Playlist> Create(Playlist playlist, string userId)
        {
            var latestPlaylistId = await _playlistRepository.GetLatestPlaylistId();
            playlist.PlaylistId = latestPlaylistId + 1;

            var newPlaylist = await _playlistRepository.Create(playlist, userId);
            var userPlaylist = new UserPlaylist
            {
                UserId = userId,
                Playlist = playlist
            };
            await _playlistRepository.AssignPlaylistToUser(userPlaylist);
            return newPlaylist;
        }

        public async Task<List<PlaylistDto>> GetAll(string userId)
        {
            var _playlist = (List<Playlist>)await _playlistRepository.GetAll(userId);
            return _playlist.Select(p => new PlaylistDto(p)).ToList();
        }

        public async Task<Playlist?> GetFavouritePlaylistByUser(string userId)
        {
            return await _playlistRepository.GetFavouritePlaylistByUser(userId);
        }


        public async Task<PlaylistDto> GetPlaylistByPlaylistAndUserId(long playlistId, string userId)
        {
            if (playlistId <= 0) throw new BadRequestException(ExceptionMessages.PlaylistIdParameterNotValidMessage());
            if (userId == null) throw new BadRequestException(ExceptionMessages.UserIdParameterNotFoundMessage());

            var playlist = await _playlistRepository.GetPlaylistByPlaylistAndUserId(playlistId, userId);
            return new PlaylistDto(playlist);
        }

        public async Task RemoveTrackFromPlaylist(long playlistId, long trackId)
        {
            if(playlistId <= 0 ) throw new BadRequestException(ExceptionMessages.PlaylistIdParameterNotValidMessage());
            if (trackId <= 0) throw new BadRequestException(ExceptionMessages.TrackIdParameterNotValidMessage());

            var playlistTrack = await _playlistRepository.GetPlaylistTrackByPlaylistAndTrackId(playlistId, trackId);
            await _playlistRepository.RemoveTrackFromPlaylist(playlistTrack);
        }

        public async Task<PlaylistDto?> GetMyFavouritePlaylist(string userId)
        {
            var favouritePlaylist = await _playlistRepository.GetMyFavouritePlaylist(userId, Constant.MyFavouritePlaylist);
            return favouritePlaylist != null ? new PlaylistDto(favouritePlaylist) : null;
        }

        public async Task AddTrackToMyFavorite(PlaylistTrackDto playlistTrackDto, string userId)
        {
            if (playlistTrackDto == null) throw new BadRequestException(ExceptionMessages.PlaylistTrackObjectNotFount());
            if (userId == null) throw new BadRequestException(ExceptionMessages.UserIdParameterNotFoundMessage());

            var favoritePlaylist = await this.GetFavouritePlaylistByUser(userId) ??
                               await this.Create(new Playlist { Name = Constant.MyFavouritePlaylist }, userId);
            var favouritePlaylistTrack = await _playlistRepository.GetPlaylistTrackByPlaylistAndTrackId(favoritePlaylist.PlaylistId, playlistTrackDto.TrackId);
            if (favouritePlaylistTrack == null)
            {
                favouritePlaylistTrack = new PlaylistTrack
                {
                    TrackId = playlistTrackDto.TrackId,
                    Playlist = favoritePlaylist,
                    IsFavorite = playlistTrackDto.IsFavorite,
                    UserId = userId
                };

                await _playlistRepository.AssignTrackToPlaylist(favouritePlaylistTrack);
            }

            var playlistTracks = await _playlistRepository.GetPlaylistTracksByTrackId(playlistTrackDto.TrackId);
            if (playlistTracks != null && playlistTracks.Count() > 0)
            {
                playlistTracks.ToList().ForEach(pt => pt.IsFavorite = true);
                await _playlistRepository.UpdatePlaylistTracks(playlistTracks);
            }
        }

        public async Task RemoveTrackFromMyFavorite(PlaylistTrackDto playlistTrackDto, string userId)
        {
            if (playlistTrackDto == null) throw new BadRequestException(ExceptionMessages.PlaylistTrackObjectNotFount());
            if (userId == null) throw new BadRequestException(ExceptionMessages.UserIdParameterNotFoundMessage());

            var favoritePlaylist = await this.GetFavouritePlaylistByUser(userId);
            var favouritePlaylistTrack = await _playlistRepository.GetPlaylistTrackByPlaylistAndTrackId(favoritePlaylist.PlaylistId, playlistTrackDto.TrackId);
            if (favouritePlaylistTrack != null)
            {
                await _playlistRepository.RemoveTrackFromPlaylist(favouritePlaylistTrack);
            }

            var playlistTracks = await _playlistRepository.GetPlaylistTracksByTrackId(playlistTrackDto.TrackId);
            if(playlistTracks != null && playlistTracks.Count() > 0)
            {
                playlistTracks.ToList().ForEach(pt => pt.IsFavorite = false);
                await _playlistRepository.UpdatePlaylistTracks(playlistTracks);
            }
        }
    }
}
