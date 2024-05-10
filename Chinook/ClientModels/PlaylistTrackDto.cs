using Chinook.Models;

namespace Chinook.ClientModels;

public class PlaylistTrackDto
{
    public long TrackId { get; set; }
    public string TrackName { get; set; }
    public string AlbumTitle { get; set; }
    public string ArtistName { get; set; }
    public bool IsFavorite { get; set; }

    public PlaylistTrackDto(Track track)
    {
        this.TrackName = track.Name;
        this.TrackId = track.TrackId;
        this.AlbumTitle = track.Album == null ? "-" : track.Album.Title;
        this.ArtistName = track.Album == null ? "-" : track.Album?.Artist?.Name ?? "-";
        this.IsFavorite = track.PlaylistTracks.Where(p => p.IsFavorite).Any();
    }
}