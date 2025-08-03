
public partial class CherylSitState : NpcState
{

    public override void Enter()
    {
        machine.data.playback.Travel(machine.data.animations["Sit"].AsString());
    }
    public override void Update(double delta)
    { 
        
    }
    public override void Exit()
    { 
        
    }
}