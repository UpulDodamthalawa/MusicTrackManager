# Chinook

This application is unfinished. Please complete below tasks. Spend max 2 hours.
We would like to have a short written explanation of the changes you made.

What I have done - Upula

Backend:

- Added Service layer(Interfaces and Concrete classes) and moved all the business logics
- Added Repository layer(Interfaces and concrete classes), moved all the database logics from pages and implemented those using entity framework core
- Implemented Exception middleware "ChinookExcepionHandler.cs" and catch and handle all the exceptions in this class
- Renamed all the client models, add suffix "<modelname>Dto" so that it can be easily identified from frontend
- Introduced new properties called IsFavorite, UserId to the "PlaylistTracks" table to persists User's favorite track preferences
- Implemented events (EventTriggerService.cs) to notify when playlist/my favorite track playlist add

Frontend:

- Show "Number of Albums" counts in the Artists page
- Added feature to Search artists by name
- Created a playlist "My favorite tracks" if not exists and shows under 'Home'
- Implemented Favourite/UnFavorite tracks 
	- From Artists --> Tracks
	- From Playlist --> Tracks
	- From My favorite tracks playlist(only UnFavorite feature to remove tracks from it)
- Implemented Add to Playlist feature
	- Shows newly added playlist in the left navigation pane real time under 'My favorite trcks'
	- Also load the playlist dropdown of the modal with the new playlist added at the same time
	- Prevent adding same playlist, shows an error message in the Modal itself
- Implemented remove track from playlist
- Fixed info messages correctly passing the parameter values
- Added Error and Warning messges

-----------------------------------------------------

1. Move data retrieval methods to separate class / classes (use dependency injection)
2. Favorite / unfavorite tracks. An automatic playlist should be created named "My favorite tracks"
3. Search for artist name

Optional:
4. The user's playlists should be listed in the left navbar. If a playlist is added (or modified), this should reflect in the left navbar (NavMenu.razor). Preferrably, this list should be refreshed without a full page reload. (suggestion: you can use Event, Reactive.NET, SectionOutlet, or any other method you prefer)
5. Add tracks to a playlist (existing or new one). The dialog is already created but not yet finished.

When creating a user account, you will see this:
"This app does not currently have a real email sender registered, see these docs for how to configure a real email sender. Normally this would be emailed: Click here to confirm your account."
After you click 'Click here to confirm your account' you should be able to login.

Please send us a zip file with the modified solution when you are done. You can also upload it to your own GitHub account and send us the link.


