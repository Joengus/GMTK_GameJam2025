using Godot;
using System;

public partial class WaterDeispensorBehavior : StaticBody3D, IInteractable
{

    [Export] Node3D prompt;
    private InteractableType iType = InteractableType.Activate;


    public void interact()
    {
        
    }
    
    public void showPrompt()
    {
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
