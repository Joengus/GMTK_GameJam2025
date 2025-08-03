using Godot;

public partial class GameManager : Node3D
{
	public static GameManager Instance;
	[Signal] public delegate void DayEndEventHandler();
	[Signal] public delegate void ResetEventHandler();

	const double TOTAL_TIME_PER_DAY = 480.0;
	public int RebelPoints { get; set; } = 0;
	public int CitizenPoints { get; set; } = 0;
	private double _gameClock = 0.0f;
	private bool _gameStarted = false;
	private bool _dayEndCalled = false;


	public override void _EnterTree()
	{
		Instance ??= this;
	}

	public override void _Process(double delta)
	{
		if (_gameStarted) _gameClock += delta;
		if (_gameClock >= TOTAL_TIME_PER_DAY && !_dayEndCalled)
		{
			EmitSignal(SignalName.DayEnd);
			_dayEndCalled = true;
		}
	}

	public void ResetDay()
	{
		EmitSignal(SignalName.Reset);
		_gameClock = 0.0;
	}

	public void StartGame()
	{
		_gameStarted = true;
	}
}
