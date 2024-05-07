using Chinook.Models;

namespace Chinook.ClientModels
{
    public class AlbumDto
    {
        public long AlbumId { get; set; }
        public string Title { get; set; } = null!;
        public long ArtistId { get; set; }

        public AlbumDto(Album album)
        {
            this.AlbumId = album.AlbumId;
            this.Title = album.Title;
            this.ArtistId = album.ArtistId;
        }
    }
}
