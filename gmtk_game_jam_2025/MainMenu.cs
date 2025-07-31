using Godot;

public partial class MainMenu : Control
{
	public static void OnPlayButtonPressed()
	{
		GD.Print("Play button pressed");
	}

	public void OnQuitButtonPressed()
	{
		GetTree().Quit();
	}
}
