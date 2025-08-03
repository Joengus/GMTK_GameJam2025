using System.Linq;
using Godot;

public partial class PlayerHUD : Control
{
	private VBoxContainer _taskPanel;
	private RichTextLabel _pointsDisplay;
	private RichTextLabel _timeDisplay;

	public override void _Ready()
	{
		_taskPanel = GetNode<VBoxContainer>("TaskPanel/VBoxContainer");
		_pointsDisplay = GetNode<RichTextLabel>("PointsDisplay");
		_timeDisplay = GetNode<RichTextLabel>("TimePanel/Time");
	}

	public override void _Process(double delta)
	{
		_timeDisplay.Text = string.Format("{0:00}", 480 - GameManager.Instance.GetTime());
	}

	public void AddTask(TaskResource task)
	{
		RichTextLabel newTask = new()
		{
			CustomMinimumSize = new(0, 30),
			Text = task.TaskName
		};
		_taskPanel.AddChild(newTask);
	}

	public void RemoveTask(string taskName)
	{
		foreach (RichTextLabel r in _taskPanel.GetChildren().Cast<RichTextLabel>())
		{
			if (r.Text == taskName) r.QueueFree();
		}
	}

	public void SetPointsDisplay(int points)
	{
		_pointsDisplay.Text = "Points: " + points;
	}
}
