using Godot;

public partial class MusicManager : AudioStreamPlayer
{
	private Music _currentPlaying = Music.CLOSET;

	public void ChangeMusic(Music newMusic)
	{
		if (newMusic == _currentPlaying) return;
		string musicToPlay;
		switch (newMusic)
		{
			case Music.CLOSET:
				musicToPlay = "supply_closet";
				break;
			case Music.OFFICE:
				musicToPlay = "office";
				break;
			case Music.EXECUTIVE:
				musicToPlay = "executive";
				break;
			default:
				GD.PrintErr($"No music enum by the name {newMusic}");
				musicToPlay = "supply_closet";
				break;
		}
		_currentPlaying = newMusic;
		Set("parameters/switch_to_clip", musicToPlay);
	}
}

public enum Music
{
	CLOSET,
	OFFICE,
	EXECUTIVE
};
