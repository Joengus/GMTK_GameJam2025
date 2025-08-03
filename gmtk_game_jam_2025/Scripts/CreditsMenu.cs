using Godot;

public partial class CreditsMenu : Control
{
	private PackedScene _mainMenu;
	private FadeController _fade;

	public override void _Ready()
	{
		_mainMenu = ResourceLoader.Load<PackedScene>("res://Scenes/main_menu.tscn");
		_fade = GetNode<FadeController>("FadeController");
	}

	public void OnBackButtonPressed()
	{
		_fade.StartFadeToBlack();
	}

	public void OnFadeFinished()
	{
		GetTree().ChangeSceneToPacked(_mainMenu);
	}
}
