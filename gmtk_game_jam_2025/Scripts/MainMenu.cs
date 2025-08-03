using Godot;

public partial class MainMenu : Control
{
	private PackedScene _playScene;
	private PackedScene _creditsScene;
	private PackedScene _sceneToLoad = null;
	private FadeController _fade;
	private Control _settingsMenu;

	public override void _Ready()
	{
		_playScene = ResourceLoader.Load<PackedScene>("res://Scenes/OfficeScene.tscn");
		_creditsScene = ResourceLoader.Load<PackedScene>("res://Scenes/credits_menu.tscn");
		_fade = GetNode<FadeController>("FadeController");
		_settingsMenu = GetNode<Control>("Settings");
	}

	public void OnPlayButtonPressed()
	{
		_fade.StartFadeToBlack();
		_sceneToLoad = _playScene;
	}

	public void OnSettingsButtonPressed()
	{
		_settingsMenu.Visible = true;
	}

	public void OnCreditsButtonPressed()
	{
		_fade.StartFadeToBlack();
		_sceneToLoad = _creditsScene;
	}

	public void OnQuitButtonPressed()
	{
		GetTree().Quit();
	}

	public void OnFadeFinished()
	{
		GetTree().ChangeSceneToPacked(_sceneToLoad);
	}
}
