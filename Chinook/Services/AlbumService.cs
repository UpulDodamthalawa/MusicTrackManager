using Chinook.ClientModels;
using Chinook.ExceptionHandlers;
using Chinook.Interfaces.Repository;
using Chinook.Interfaces.Service;

namespace Chinook.Services
{
    public class AlbumService : IAlbumService
    {
        private readonly IAlbumRepository _albumRepository;

        public AlbumService(IAlbumRepository albumRepository)
        {
            _albumRepository = albumRepository;
        }
        public async Task<List<AlbumDto>> GetAlbumsByArtist(long artistId)
        {
            if (artistId <= 0) throw new BadRequestException(ExceptionMessages.ArtistIdParameterNotValidMessage());

            var albums = await _albumRepository.GetAlbumsByArtist(artistId);
            return albums.Select(a => new AlbumDto(a)).ToList();
        }
    }
}
