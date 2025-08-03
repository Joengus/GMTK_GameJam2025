
using Godot;

public partial class TaskObject : Node3D
{
    [Export] public TaskResource[] linkedTask;

    public override void _Ready()
    {
        foreach (TaskResource task in linkedTask)
        {
            GD.Print(task.TaskName);
            TaskManager.Instance.AddObjectToTask(task, this);
        }
    }
}