using Godot;

public partial class TaskManager : Node3D
{
	public static TaskManager Instance;
	[Export] public TaskResource[] TaskList;

	public override void _EnterTree()
	{
		Instance ??= this;
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
}
