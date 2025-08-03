
public partial class CherylWalkState : NpcState
{

    public override void Enter()
    {
        base.Enter();
        machine.data.playback.Play(machine.data.animations["Walk"].AsString());
    }
    public override void Update(double delta)
    { 
        
    }
    public override void Exit()
    { 
        machine.data.playback.Stop();
        
    }
}