using Godot;

public partial class FadeController : CanvasLayer
{
	[Signal] public delegate void FadeToBlackEventHandler();
	private AnimationPlayer _animationPlayer;
	private string _fadeToBlack = "fade_to_black";
	private string _fadeToClear = "fade_to_normal";

	public override void _Ready()
	{
		_animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		_animationPlayer.Play(_fadeToClear);
	}

	public void StartFadeToBlack()
	{
		_animationPlayer.Play(_fadeToBlack);
		Layer = 1;
	}

	public void OnAnimationFinished(StringName animationName)
	{
		if (animationName == _fadeToBlack)
		{
			EmitSignal(SignalName.FadeToBlack);
		}
		else if (animationName == _fadeToClear)
		{
			Layer = -100;
		}
	}
}
