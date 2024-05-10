using Chinook.Models;

namespace Chinook.ClientModels;

public class PlaylistDto
{
    public long PlaylistId { get; set; }
    public string Name { get; set; }
    public List<PlaylistTrackDto> Tracks { get; set; }

    public PlaylistDto(Playlist playlist)
    {
        if (playlist != null)
        {
            this.PlaylistId = playlist.PlaylistId;
            this.Name = playlist.Name?? " ";
            this.Tracks = playlist.PlaylistTracks.Where(c => c != null).Select(c => new PlaylistTrackDto(c.Track)).ToList();
        }
    }
}