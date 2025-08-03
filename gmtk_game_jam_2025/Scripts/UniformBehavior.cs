using Godot;
using System;

public partial class UniformBehavior : StaticBody3D, IInteractable
{

    [Export] Node3D prompt;
    [Export] Node3D taskObject;
    private InteractableType iType = InteractableType.Activate;


    public void interact()
    {
        //activate end of day
        if (taskObject is TaskObject task)
        {
            TaskManager.Instance.CompleteTask(task.linkedTask[0]);
        }
    }
    
    public void showPrompt()
    {
        GD.Print("Show Prompt");
        prompt.Visible = true;
    }
    

    public void hidePrompt()
    {
        prompt.Visible = false;
    }

    public InteractableType getIType()
    {
        return iType;
    }
}
