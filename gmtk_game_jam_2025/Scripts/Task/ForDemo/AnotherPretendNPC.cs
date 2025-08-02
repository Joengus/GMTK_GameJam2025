using Godot;
using System;

public partial class AnotherPretendNPC : Node3D
{
	// idk ... private string npc_name_i_guess = uhh bob;;
	// Other hidden NPC values cuz i said so --> rekt lmao :p  

	[Signal] public delegate void GetDressedEventHandler();
	[Export] private TaskResource[] _associatedTasks;

	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("Right"))
		{
			GD.Print($"[AnotherPretendNPC]\nTask name : {_associatedTasks[0].TaskName}");
			GD.Print($"Task complete : {_associatedTasks[0].IsComplete}\n");
			if (!_associatedTasks[0].IsComplete)
			{
				TaskManager.Instance.CompleteTask(_associatedTasks[0]);
				EmitSignal(SignalName.GetDressed);
			}
		}
	}
}
