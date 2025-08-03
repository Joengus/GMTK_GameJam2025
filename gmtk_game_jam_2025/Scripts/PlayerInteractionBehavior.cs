using Godot;
using System;
using System.Collections;
using System.Diagnostics;

public partial class PlayerInteractionBehavior : Node3D
{
    public static PlayerInteractionBehavior instance;

    [Export] ShapeCast3D shapeCast;
    [Export] ShapeCast3D slapCast;
    PhysicsBody3D currentSelectedInteractable = null;
    bool ItemHeld = false;
    PhysicsBody3D currentHeldObject = null;
    [Export] Node3D handTransform;
    [Export] AnimationPlayer anim;

    public override void _EnterTree()
    {
        base._EnterTree();
        if (instance == null)
        {
            instance = this;
        }
    }


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
            if (script.getIType() == InteractableType.Hold)//&& currentHeldObject == null)
            {
                if (currentHeldObject != null)
                {
                    currentHeldObject.Reparent(GetTree().CurrentScene, true);
                }
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
        else
        {
            //anim.Play("PC_throw", 1, 3);
            slap();
        }
    }

    private async void slap()
    {
        anim.Play("Slap_ANIM", 0, 4);

        while (anim.CurrentAnimationPosition < anim.CurrentAnimation.Length / 8)
        {
            await ToSignal(GetTree(), "process_frame"); 
        }

        slapCast.ForceShapecastUpdate();
        int numOfHits = slapCast.GetCollisionCount();

        if (numOfHits > 0)
        {
            for (int i = 0; i < numOfHits; i++)
            {
                Node3D collider = slapCast.GetCollider(i) as Node3D;

                if (collider is RigidBody3D rb)
                {
                    rb.ApplyCentralImpulse(-slapCast.GlobalBasis.Z * 10);
                }
            }
        }

        while (anim.CurrentAnimationPosition < anim.CurrentAnimation.Length)
        {
            await ToSignal(GetTree(), "process_frame"); 
        }  
        
        anim.Play("Slap_STATIC", .25, 1);
    }
}
