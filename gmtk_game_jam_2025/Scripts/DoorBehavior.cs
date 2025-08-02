using Godot;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

public partial class DoorBehavior : StaticBody3D, IInteractable
{
    [Export] public Tween.TransitionType TransitionType;
    [Export] Node3D openPrompt;
    [Export] Node3D lockedPrompt;
    [Export] Node3D openPos;
    [Export] Node3D closedPos;
    private bool active = false;
    [Export] bool unlocked = true;
    private float _tweenDuration = 1f; // Example duration


    private InteractableType iType = InteractableType.Activate;

    public override void _Ready()
    {
        base._Ready();
    }

    public void interact()
    {
        if (!active && unlocked)
        {
            active = true;
            hidePrompt();
            
            Tween newTween = CreateTween();

            newTween.SetTrans(TransitionType);

            newTween.TweenProperty(this, "global_position", openPos.GlobalPosition, _tweenDuration);
            newTween.Chain().TweenProperty(this, "global_position", closedPos.GlobalPosition, _tweenDuration);
            newTween.Connect("finished", new Callable(this, nameof(OnTweenFinished))); // Connect the finished signal

            newTween.Play();
        }
    }

    private void OnTweenFinished()
    {
        active = false;
        GD.Print("Object reached its target position!");
    }

    public void showPrompt()
    {
        if (!active)
        {
            if (unlocked)
            {
                openPrompt.Visible = true;
            }
            else
            {
                lockedPrompt.Visible = true;
            }
        }
    }

    public void hidePrompt()
    {
        openPrompt.Visible = false;
        lockedPrompt.Visible = true;
    }

    public InteractableType getIType()
    {
        return iType;
    }
}
