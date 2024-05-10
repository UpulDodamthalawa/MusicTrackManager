namespace Chinook.Models
{
    public class PlaylistTrack
    {
        public long TrackId { get; set; }
        public long PlaylistId { get; set; }
        public bool IsFavorite { get; set; }
        public string? UserId { get; set; }
        public Track? Track { get; set; }
        public Playlist? Playlist { get; set; }
    }
}
