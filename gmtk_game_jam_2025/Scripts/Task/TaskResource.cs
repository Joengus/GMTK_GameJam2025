using Godot;

[GlobalClass]
public partial class TaskResource : Resource
{
	public enum ReputationCategory { REBEL, NEUTRAL, CITIZEN };
	[Export] public ReputationCategory ReputationAlignment;
	[Export] public string TaskName;
	[Export] public int PointsTowardAlignment;
	[Export] public string DescriptionRebel;
	[Export] public string DescriptionNeutral;
	[Export] public string DescriptionCitizen;
	[Export] public TaskResource[] PrerequisiteTasks;
	[Export] public bool isHidden;
	public bool IsComplete = false;
}
