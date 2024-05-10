using Chinook.ClientModels;

namespace Chinook.Interfaces.Service
{
    public interface IArtistService
    {
        Task<List<ArtistDto>?> GetAll();

        Task<ArtistDto?> GetArtistById(long artistId);
    }
}
