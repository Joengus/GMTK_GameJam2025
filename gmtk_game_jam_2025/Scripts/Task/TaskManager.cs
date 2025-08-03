using Godot;

public partial class TaskManager : Node3D
{
	public static TaskManager Instance;

	private const int NON_NEUTRAL_FLAVOR_TEXT_POINT_THRESHOLD = 50;
	[Export] public TaskResource[] TaskList;
	private RichTextLabel _flavorText;

	public override void _EnterTree()
	{
		Instance ??= this;
	}

	public override void _Ready()
	{
		_flavorText = GetNode<RichTextLabel>("Control/Text");
	}

	public void CompleteTask(TaskResource taskToMarkComplete)
	{
		TaskResource task = FindTask(taskToMarkComplete);
		if (task == null) return;
		foreach (TaskResource t in taskToMarkComplete.PrerequisiteTasks)
		{
			if (!t.IsComplete) return;
		}
		task.IsComplete = true;
		DisplayFlavorText(taskToMarkComplete);
	}

	private TaskResource FindTask(TaskResource taskToFind)
	{
		foreach (TaskResource t in TaskList)
		{
			if (t == taskToFind)
				return t;
		}
		GD.PrintErr($"Could not find task '{taskToFind.TaskName}'.");
		return null;
	}

	private void DisplayFlavorText(TaskResource task)
	{
		if (GameManager.Instance.CitizenPoints >= NON_NEUTRAL_FLAVOR_TEXT_POINT_THRESHOLD)
		{
			_flavorText.Text = task.DescriptionCitizen;
		}
		else if (GameManager.Instance.RebelPoints >= NON_NEUTRAL_FLAVOR_TEXT_POINT_THRESHOLD)
		{
			_flavorText.Text = task.DescriptionRebel;
		}
		else
		{
			_flavorText.Text = task.DescriptionNeutral;
		}
	}
}
