using Godot;
using System;

public partial class MoveableBehavior : RigidBody3D, IInteractable
{
    [Export] Label3D prompt;
    public InteractableType interactType = InteractableType.Move;

    public void showPrompt()
    {
        prompt.Visible = true;
    }

    public void hidePrompt()
    {
        prompt.Visible = false;
    }

    public void interact()
    {

    }

    public InteractableType getIType()
    {
        return interactType;
    }
}
