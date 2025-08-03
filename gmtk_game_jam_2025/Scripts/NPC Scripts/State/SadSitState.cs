
public partial class CherylSitState : NpcState
{

    public override void Enter()
    {
        machine.data.playback.Play(machine.data.animations["Sit"].AsString());
    }
    public override void Update(double delta)
    { 
        
    }
    public override void Exit()
    { 
        machine.data.playback.Stop();
        
    }
}