namespace Chinook.ExceptionHandlers
{
    public class ExceptionMessages
    {
        public static string UserNotFoundMessage(string userId) => $"User with id '{userId}' could not be found.";
        public static string PlaylistNotFoundMessage(long playlistId) => $"Playlist with id '{playlistId}' could not be found.";
        public static string TrackNotFoundMessage(long trackId) => $"Track {trackId} not found.";
        public static string PlaylistTrackObjectNotFount() => $"PlaylistTrack object not found.";
        public static string ArtistIdParameterNotValidMessage() => $"ArtistId parameter is required";
        public static string UserIdParameterNotFoundMessage() => $"UserId parameter is required";
        public static string TrackIdParameterNotValidMessage() => $"TrackId parameter is not valid";
        public static string PlaylistIdParameterNotValidMessage() => $"PlaylistId parameter is not valid";
    }
}
