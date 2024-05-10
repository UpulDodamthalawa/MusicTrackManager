using Chinook.Models;

namespace Chinook.ClientModels
{
    public class ArtistDto
    {
        public long ArtistId { get; set; }
        public string? Name { get; set; }

        public virtual ICollection<Album> Albums { get; set; }

        public ArtistDto(Artist artist)
        {
            this.Name = artist.Name;
            this.ArtistId = artist.ArtistId;
            this.Albums = artist.Albums;
        }
    }
}
