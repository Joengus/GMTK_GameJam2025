using Godot;
using System;

public partial class Pout0State : NpcState
{
    public override void Enter()
    {
        machine.data.playback.Play(machine.data.animations["Pout_0"].AsStringName());
    }
    public override void Update(double delta) { }
    public override void Exit() { machine.data.playback.Stop(); }
}
