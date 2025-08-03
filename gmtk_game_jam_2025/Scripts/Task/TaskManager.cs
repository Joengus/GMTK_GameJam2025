using System.Collections.Generic;
using Godot;
using Godot.Collections;

public partial class TaskManager : Node3D
{
	public static TaskManager Instance;

	private const int NON_NEUTRAL_FLAVOR_TEXT_POINT_THRESHOLD = 50;
	[Export] public Dictionary GlobalTaskList = new Dictionary();
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
		if (!GlobalTaskList.ContainsKey(taskToMarkComplete))
		{
			GD.PrintErr($"Task does not exist: '{taskToMarkComplete.TaskName}'");
			return;
		}
		foreach (TaskResource t in taskToMarkComplete.PrerequisiteTasks)
		{
			if (!t.IsComplete) return;
		}
		taskToMarkComplete.IsComplete = true;
		DisplayFlavorText(taskToMarkComplete);
	}

	public Array<Node3D> GetPossibleTargets(TaskResource task)
	{
		return GlobalTaskList[task].AsGodotArray<Node3D>();
	}

	public void AddObjectToTask(TaskResource task, Node3D node)
	{
		GlobalTaskList[task].AsGodotArray<Node3D>().Add(node);
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
