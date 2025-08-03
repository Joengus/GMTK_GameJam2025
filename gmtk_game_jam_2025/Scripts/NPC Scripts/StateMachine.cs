using Godot;
using System;


public partial class StateMachine : Node3D
{
    [Export] NpcData Npc;
    [Export] public NpcState currentState;
    public NpcState previousState;

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
        currentState.Update(delta);
    }

    public void ChangeState(NpcState newState)
    {
        previousState = currentState;
        currentState = newState;
        previousState.Exit();
        currentState.Enter();
    }
}
