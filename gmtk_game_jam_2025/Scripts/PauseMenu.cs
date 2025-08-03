using Godot;

public partial class PauseMenu : Control
{
	[Export] public PackedScene MainMenuScene;

	public void OnResumeButtonPressed()
	{
		Visible = false;
		Input.MouseMode = Input.MouseModeEnum.Captured;
	}

	public void OnMainMenuButtonPressed()
	{
		GetTree().ChangeSceneToPacked(MainMenuScene);
	}

	public void TogglePauseMenu()
	{
		Visible = !Visible;
	}
}
