using Godot;
using System;

public partial class LieDownState : NpcState
{
    public override void Enter()
    {
        machine.data.playback.Play(machine.data.animations["lie down"].AsStringName());
    }
    public override void Update(double delta) { }
    public override void Exit() { machine.data.playback.Stop(); }
}
