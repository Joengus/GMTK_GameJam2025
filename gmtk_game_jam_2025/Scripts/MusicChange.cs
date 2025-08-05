using Godot;

public partial class MusicChange : Area3D
{
	[Export] private Music _musicToPlay;
	private AudioStreamPlaybackInteractive _musicPlayer;

	public override void _Ready()
	{
		_musicPlayer = (AudioStreamPlaybackInteractive)GetNode<AudioStreamPlayer>("../MusicPlayer").GetStreamPlayback();
	}

	public void OnCollisionEnter(Node3D other)
	{
		if (other.GetParent().Name == "PlayerCharacter")
		{
			if (_musicPlayer.GetCurrentClipIndex() == (int)_musicToPlay) return;
			_musicPlayer.SwitchToClip((int)_musicToPlay);
		}
	}
}

public enum Music
{
	CLOSET,
	OFFICE,
	EXECUTIVE
};
