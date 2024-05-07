using Chinook.ClientModels;
using Chinook.ExceptionHandlers;
using Chinook.Interfaces.Repository;
using Chinook.Interfaces.Service;

namespace Chinook.Services
{
    public class ArtistService : IArtistService
    {
        private readonly IArtistRepository _artistRepository;

        public ArtistService(IArtistRepository artistRepository)
        {
            _artistRepository = artistRepository;
        }
        public async Task<List<ArtistDto>?> GetAll()
        {
            var artists = await _artistRepository.GetAll();
            return artists != null ? artists.Select(a=> new ArtistDto(a)).ToList() : null;
        }

        public async Task<ArtistDto?> GetArtistById(long artistId)
        {
            if (artistId <= 0) throw new BadRequestException(ExceptionMessages.ArtistIdParameterNotValidMessage());

            var artist = await _artistRepository.GetArtistById(artistId);
            return  artist != null ?new ArtistDto(artist) : null;
        }
    }
}
