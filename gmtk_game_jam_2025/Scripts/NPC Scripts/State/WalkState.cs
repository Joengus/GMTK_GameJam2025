

using Godot;

public partial class WalkState : NpcState
{
    private Node3D targetReference;
    public override void _Ready()
    {
        base._Ready();
        machine.data.agent.VelocityComputed += VelocityComputed;
        targetReference = TaskManager.Instance.GetPossibleTargets(machine.data.taskList[0])[0];
        GD.Print(targetReference.GlobalPosition);

    }
    public override void Enter()
    {
        base.Enter();
        machine.data.playback.Play(machine.data.animations["Walk"].AsString());
    }
    public override void Update(double delta)
    {
        machine.data.agent.TargetPosition = targetReference.GlobalPosition;
        Vector3 currentLocation = machine.data.body3D.Position;
        Vector3 nextLocation = machine.data.agent.GetNextPathPosition();
        Vector3 newVel = (nextLocation - currentLocation) * machine.data.walkSpeed * (float)delta;
        GD.Print(newVel);
        VelocityComputed(newVel);
    }
    public override void Exit()
    {
        machine.data.playback.Stop();

    }

    private void VelocityComputed(Vector3 safeVelocity)
    {
        machine.data.body3D.Velocity.MoveToward(safeVelocity, 0.25f);
        machine.data.body3D.MoveAndSlide();
    }
}