using Chinook.Interfaces.EventTrigger;

namespace Chinook.Services
{
    public class EventTriggerService : IEventTriggerService
    {
        public event EventHandler? OnMyFavouritePlaylistAdded;
        public event EventHandler? OnPlaylistAdded;

        public void TriggerMyFavouritePlaylistAdded()
        {
            OnMyFavouritePlaylistAdded?.Invoke(this, EventArgs.Empty);
        }

        public void TriggerPlaylistAdded()
        {
            OnPlaylistAdded?.Invoke(this, EventArgs.Empty);
        }
    }
}
