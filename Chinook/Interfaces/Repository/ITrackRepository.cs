using Chinook.Models;

namespace Chinook.Interfaces.Repository
{
    public interface ITrackRepository
    {
        Task<IEnumerable<Track>> GetTracksByArtistAndUserId(long artistId, string userId);

        Task<Track?> GetById(long id);
    }
}
