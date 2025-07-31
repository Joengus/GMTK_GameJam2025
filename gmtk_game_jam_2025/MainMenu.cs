using Godot;

public partial class MainMenu : Control
{
	public void OnPlayButtonPressed()
	{
		GD.Print("Play button pressed");
	}

	public void OnQuitButtonPressed()
	{
		GetTree().Quit();
	}
}
