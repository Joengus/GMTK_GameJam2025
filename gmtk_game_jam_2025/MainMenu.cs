using Godot;

public partial class MainMenu : Control
{
	private Control _settingsMenu;

	public override void _Ready()
	{
		_settingsMenu = GetNode<Control>("Settings");
	}

	public void OnPlayButtonPressed()
	{
		GD.Print("Play button pressed");
	}

	public void OnSettingsButtonPressed()
	{
		_settingsMenu.Visible = true;
	}

	public void OnQuitButtonPressed()
	{
		GetTree().Quit();
	}
}
