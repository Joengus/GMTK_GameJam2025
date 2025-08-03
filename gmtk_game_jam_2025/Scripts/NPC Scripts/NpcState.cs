
using Godot;

public partial class NpcState : Node3D
{
    private static int StateCounter;
    public int id;
    string AnimationName;
    public NpcData data;
    public NpcState(NpcData data)
    {
        this.data = data;
        this.id = NpcState.StateCounter++;
    }

    public virtual void Enter() { }
    public virtual void Update(double delta) { }
    public virtual void Exit() { }

    public static implicit operator int(NpcState state)
    {
        return state.id;
    }
}
