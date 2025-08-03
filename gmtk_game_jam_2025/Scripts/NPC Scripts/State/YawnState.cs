using Godot;
using System;

public partial class YawnState : NpcState
{
    public override void Enter()
    {
        machine.data.playback.Play(machine.data.animations["Yawn"].AsStringName());
    }
    public override void Update(double delta) { }
    public override void Exit() { machine.data.playback.Stop(); }
}
