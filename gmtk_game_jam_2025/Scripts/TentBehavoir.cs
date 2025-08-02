using Godot;
using System;

public partial class TentBehavoir : StaticBody3D, IInteractable
{

    [Export] Node3D SleepPrompt;
    private InteractableType iType = InteractableType.Activate;


    public void interact()
    {
        //activate end of day 
    }
    
    public void showPrompt()
    {
        SleepPrompt.Visible = true;
    }
    

    public void hidePrompt()
    {
        SleepPrompt.Visible = false;
    }

    public InteractableType getIType()
    {
        return iType;
    }
}
