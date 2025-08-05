using Godot;

public partial class MusicChange : Area3D
{
	[Export] private Music _music;

	public void OnCollisionEnter(Node3D other)
	{
		if (other.GetParent().Name == "PlayerCharacter")
		{
			GetNode<MusicManager>("../MusicPlayer").ChangeMusic(_music);
		}
	}
}
