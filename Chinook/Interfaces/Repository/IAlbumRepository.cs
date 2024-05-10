using Chinook.Models;

namespace Chinook.Interfaces.Repository
{
    public interface IAlbumRepository
    {
        public Task<IEnumerable<Album>> GetAlbumsByArtist(long artistId);
    }
}
