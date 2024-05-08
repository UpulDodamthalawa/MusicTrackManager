namespace Chinook.Interfaces.EventTrigger
{
    public interface IEventTriggerService
    {
        event EventHandler OnMyFavouritePlaylistAdded;
        event EventHandler OnPlaylistAdded;

        void TriggerMyFavouritePlaylistAdded();
        void TriggerPlaylistAdded();
    }
}
