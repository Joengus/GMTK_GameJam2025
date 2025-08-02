using Godot;
using System;
using System.Diagnostics;

public partial class PlayerInteractionBehavior : Node3D
{

    [Export] ShapeCast3D shapeCast;
    PhysicsBody3D currentSelectedInteractable = null;
    bool ItemHeld = false;
    PhysicsBody3D currentHeldObject = null;
    [Export] Node3D handTransform;

    public override void _Ready()
    {
        base._Ready();
    }


    public override void _Process(double delta)
    {
        base._Process(delta);

        detectInteractables();
        heldObjectFollow();
    }

    private void detectInteractables()
    {
        //do raycast to detect interactable objects
        shapeCast.ForceShapecastUpdate();
        if (shapeCast.GetCollisionCount() > 0)
        {
            var collider = shapeCast.GetCollider(0);
            if (collider is IInteractable interactable && interactable != currentSelectedInteractable && interactable != currentHeldObject)
            {
                if (currentSelectedInteractable != null && currentSelectedInteractable is IInteractable script)
                {
                    script.hidePrompt();
                }
                interactable.showPrompt();
                currentSelectedInteractable = (PhysicsBody3D)collider;
            }
        }
        else
        {
            if (currentSelectedInteractable != null && currentSelectedInteractable is IInteractable script)
            {
                script.hidePrompt();
                currentSelectedInteractable = null;
            }
        }
    }

    private void heldObjectFollow()
    {
        if (currentHeldObject != null && currentHeldObject.GlobalPosition != handTransform.GlobalPosition)
        {
            currentHeldObject.GlobalPosition = handTransform.GlobalPosition;
            currentHeldObject.GlobalRotation = handTransform.GlobalRotation;
        }
    }

    public void _Interact()
    {
        if (currentSelectedInteractable is IInteractable script)
        {
            if (script.getIType() == InteractableType.Hold && currentHeldObject == null)
            {
                //grab
                script.hidePrompt();
                currentSelectedInteractable.Reparent(handTransform, false);
                currentHeldObject = (RigidBody3D)currentSelectedInteractable;
                currentSelectedInteractable = null;
            }
            else if (script.getIType() == InteractableType.Activate)
            {
                script.interact();
            }            
        }

    }

    public void throwSlap(Vector3 direction, Vector3 position)
    {
        if (currentHeldObject != null)
        {
            currentHeldObject.Reparent(GetTree().CurrentScene, true);
            RigidBody3D obj = (RigidBody3D)currentHeldObject;
            currentHeldObject = null;

            obj.GlobalPosition = position + direction;
            obj.LinearVelocity = Vector3.Zero;
            obj.ApplyCentralImpulse(direction * 20);
        }
    }
}
