namespace Chinook.ClientModels
{
    public class TrackDto
    {
        public long TrackId { get; set; }
        public string Name { get; set; } = null!;
        public AlbumDto Album { get; set; }
        public virtual ICollection<PlaylistDto> Playlists { get; set; }
    }
}
