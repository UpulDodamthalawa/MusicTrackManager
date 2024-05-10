using Chinook.Models;

namespace Chinook.Interfaces.Repository
{
    public interface IArtistRepository
    {
        public Task<IEnumerable<Artist>> GetAll();

        Task<Artist?> GetArtistById(long artistId);
    }
}
