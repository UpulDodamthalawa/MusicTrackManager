using Chinook.ClientModels;

namespace Chinook.Interfaces.Service
{
    public interface IAlbumService
    {
        public Task<List<AlbumDto>> GetAlbumsByArtist(long artistId);
    }
}
