public partial class CherylIdleState : NpcState
{

    public override void Enter()
    {
        machine.data.playback.Travel(machine.data.animations["Idle"].AsString());
    }
    public override void Update(double delta)
    { 
        
    }
    public override void Exit()
    { 
        
    }
}