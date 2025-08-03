
using Godot;

public partial class TaskObject : Node3D
{
    [Export] public TaskResource[] linkedTask;

    public override void _EnterTree()
    {
        foreach (TaskResource task in linkedTask)
        {
            TaskManager.Instance.AddObjectToTask(task, this);
        }
    }
}