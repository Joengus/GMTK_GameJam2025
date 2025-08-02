using Godot;

public partial class PretendNpc : Node3D
{
	// idk ... private string npc_name_i_guess = uhh bob;;
	// Other hidden NPC values cuz i said so --> rekt lmao :p  

	[Export] private TaskResource[] _associatedTasks;

	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("Left"))
		{
			GD.Print($"[PretendNPC]\nTask name : {_associatedTasks[0].TaskName}");
			GD.Print($"Task complete : {_associatedTasks[0].IsComplete}\n");
			TaskManager.Instance.CompleteTask(_associatedTasks[0]);
		}
	}
}
