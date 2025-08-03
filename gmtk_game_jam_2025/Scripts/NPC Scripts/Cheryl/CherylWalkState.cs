
public partial class CherylWalkState : NpcState
{

    public override void Enter()
    {
        base.Enter();
        machine.data.playback.Travel(machine.data.animations["Walk"].AsString());
    }
    public override void Update(double delta)
    { 
        
    }
    public override void Exit()
    { 
        
    }
}