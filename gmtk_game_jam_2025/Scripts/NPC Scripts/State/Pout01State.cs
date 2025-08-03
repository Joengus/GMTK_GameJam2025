using Godot;
using System;

public partial class Pout01State : NpcState
{
    public override void Enter()
    {
        machine.data.playback.Play(machine.data.animations["Pout_1"].AsStringName());
    }
    public override void Update(double delta) { }
    public override void Exit() { machine.data.playback.Stop(); }
}
