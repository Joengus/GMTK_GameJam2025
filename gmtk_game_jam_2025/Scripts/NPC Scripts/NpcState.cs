
using Godot;

public partial class NpcState : Node3D
{
    private static int StateCounter;
    protected StateMachine machine;
    public int id;
    string AnimationName;
    public NpcState()
    {
        this.id = NpcState.StateCounter++;
    }

    public override void _Ready()
    {
        base._Ready();
        machine = GetParent<StateMachine>();
    }   

    public virtual void Enter() { }
    public virtual void Update(double delta) { }
    public virtual void Exit() { }

    public static implicit operator int(NpcState state)
    {
        return state.id;
    }
}
