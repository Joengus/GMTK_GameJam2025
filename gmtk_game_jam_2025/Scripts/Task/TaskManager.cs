using System.Collections.Generic;
using Godot;
using Godot.Collections;

public partial class TaskManager : Node3D
{
	public static TaskManager Instance;
	[Export] public Dictionary GlobalTaskList = new Dictionary();

	public override void _EnterTree()
	{
		Instance ??= this;
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
	}

	public Array<Node3D> GetPossibleTargets(TaskResource task)
	{
		return GlobalTaskList[task].AsGodotArray<Node3D>();
	}

	public void AddObjectToTask(TaskResource task, Node3D node)
	{
		GlobalTaskList[task].AsGodotArray<Node3D>().Add(node);
	}
}
