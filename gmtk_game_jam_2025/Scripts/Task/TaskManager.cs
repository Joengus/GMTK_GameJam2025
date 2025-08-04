using System;
using System.Collections.Generic;
using Godot;
using Godot.Collections;

public partial class TaskManager : Node3D
{
	public static TaskManager Instance;

	private const int NON_NEUTRAL_FLAVOR_TEXT_POINT_THRESHOLD = 50;
	[Export] TaskLibraryResource taskLibrary;
	[Export] public Dictionary GlobalTaskList = new Dictionary();
	public List<TaskResource> currentTaskList = new List<TaskResource>();
	private RichTextLabel _flavorText;

	public override void _EnterTree()
	{
		Instance ??= this;
	}

	public override void _Ready()
	{
		_flavorText = GetNode<RichTextLabel>("Control/Text");
		AssignTasks();
		foreach (TaskResource task in currentTaskList)
		{
			GD.Print("New Task: " + task.TaskName);			
		}
	}

	public void AssignTasks()
	{
		currentTaskList.Clear();
		currentTaskList = taskLibrary.GetRandomIncompleteTasks(5);
	}

	public void CompleteTask(TaskResource taskToMarkComplete)
	{
		foreach (TaskResource t in taskToMarkComplete.PrerequisiteTasks)
		{
			if (!t.IsComplete) return;
		}
//		GD.Print("Task Complete: " + taskToMarkComplete.TaskName);
		taskToMarkComplete.IsComplete = true;
		DisplayFlavorText(taskToMarkComplete);
	}

	public Array<Node3D> GetPossibleTargets(TaskResource task)
	{
		if (GlobalTaskList.ContainsKey(task))
		{
//			GD.Print("Found task: " + task.TaskName);
			return GlobalTaskList[task].AsGodotArray<Node3D>();
		}
		else
		{
//			GD.Print("Couldn't Find task: " + task.TaskName);
			return null;
		}
	}

	public void AddObjectToTask(TaskResource task, Node3D node)
	{
		if (GlobalTaskList.ContainsKey(task))
		{
//			GD.Print("Found task: " + task.TaskName);
			GlobalTaskList[task].AsGodotArray<Node3D>().Add(node);
		}
		else
		{
//			GD.Print("Couldn't Find task: " + task.TaskName);
		}
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
