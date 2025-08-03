using Godot;

public partial class FlavorText : Control
{
	private const double MAX_TEXT_TIME = 3.0;
	private RichTextLabel _text;
	private double _textShownTimer = 0.0;

	public override void _Ready()
	{
		_text = GetNode<RichTextLabel>("Text");
	}

	public override void _Process(double delta)
	{
		if (_text.Text != "")
		{
			_textShownTimer += delta;
			if (_textShownTimer >= MAX_TEXT_TIME)
			{
				_text.Text = "";
				_textShownTimer = 0.0;
			}
		}
	}
}
