using System;
using System.Collections.Generic;
using Godot;
using Godot.Collections;

public partial class TaskManager : Node3D
{
	public static TaskManager Instance;

	private const int NON_NEUTRAL_FLAVOR_TEXT_POINT_THRESHOLD = 50;
	[Export] public Dictionary GlobalTaskList = new Dictionary();
	public List<Object> currentTaskKeys = new List<Object>();
	private RichTextLabel _flavorText;

	public override void _EnterTree()
	{
		Instance ??= this;
	}

	public override void _Ready()
	{
		_flavorText = GetNode<RichTextLabel>("Control/Text");
		AssignTasks();
		foreach (TaskResource task in currentTaskKeys)
		{
			GD.Print("New Task: " + task.TaskName);			
		}
	}

	public void AssignTasks()
	{
		currentTaskKeys.Clear();
		for (int i = 0; i < 10; i++)
		{
			var rng = new RandomNumberGenerator();
			// Get keys as a Godot.Collections.Array
			Godot.Collections.Array keysArray = new Godot.Collections.Array(GlobalTaskList.Keys);

			// Get values as a Godot.Collections.Array
			Godot.Collections.Array valuesArray = new Godot.Collections.Array(GlobalTaskList.Values);
			bool gotGoodTask = false;

			while (!gotGoodTask)
			{
				GD.Print("In while loop");
				rng.Randomize();
				int randomNumber = rng.RandiRange(0, keysArray.Count - 1);
				TaskResource taskInfo = (TaskResource)keysArray[randomNumber];
				if (!taskInfo.isHidden && !currentTaskKeys.Contains(keysArray[randomNumber]))
				{
					gotGoodTask = true;
					currentTaskKeys.Add(keysArray[randomNumber]);
				}
			}
		}



		GD.Print("Number of tasks: " + currentTaskKeys.Count);
	}

	public void CompleteTask(TaskResource taskToMarkComplete)
	{
		if (!GlobalTaskList.ContainsKey(taskToMarkComplete))
		{
//			GD.PrintErr($"Task does not exist: '{taskToMarkComplete.TaskName}'");
			return;
		}
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
