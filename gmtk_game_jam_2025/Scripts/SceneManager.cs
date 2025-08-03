using Godot;

public partial class SceneManager : Node3D
{
	private PauseMenu _pauseMenu;
	private bool _pauseMenuOpen = false;

	public override void _Ready()
	{
		_pauseMenu = GetNode<PauseMenu>("../PauseMenu");
	}

	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("Pause"))
		{
			_pauseMenu.TogglePauseMenu();
		}
	}
}
