

using Godot;

public partial class WalkState : NpcState
{
    private Vector3 desiredVel;
    private Node3D targetReference;
    public override void _Ready()
    {
        base._Ready();
        machine.data.agent.VelocityComputed += VelocityComputed;
        targetReference = TaskManager.Instance.GetPossibleTargets(machine.data.taskList[0])[0];
        GD.Print(targetReference.GlobalPosition);
        machine.data.agent.TargetPosition = targetReference.GlobalPosition;
        machine.data.agent.AvoidanceEnabled = true;
        machine.data.agent.SetProcess(true);
        machine.data.agent.SetPhysicsProcess(true);

    }
    public override void Enter()
    {
        base.Enter();
        machine.data.playback.Play(machine.data.animations["Walk"].AsString());
    }
    public override void Update(double delta)
    {
        Vector3 currentLocation = machine.data.body3D.Position;
        Vector3 nextLocation = machine.data.agent.GetNextPathPosition();
        Vector3 newVel = (nextLocation - currentLocation).Normalized() * machine.data.walkSpeed * (float)delta;
        machine.data.agent.Velocity = newVel;

        Vector3 direction = machine.data.agent.Velocity.Normalized();

        // Ignore vertical component to keep rotation flat on Y-axis
        direction.Y = 0;

        if (direction.LengthSquared() > 0.001f)
        {
            // Calculate the angle in radians
            float targetAngle = Mathf.Atan2(direction.X, direction.Z);

            // Set only the Y rotation
            Vector3 currentRotation = machine.data.body3D.Rotation;
            currentRotation.Y = targetAngle;
            machine.data.body3D.Rotation = currentRotation;
        }
    }
    public override void Exit()
    {
        machine.data.playback.Stop();
    }

    private void VelocityComputed(Vector3 safetyVelocity)
    {
        GD.Print("Safe velocity: ", safetyVelocity);
        machine.data.body3D.Velocity = machine.data.body3D.Velocity.MoveToward(safetyVelocity, 0.25f);
        machine.data.body3D.MoveAndSlide();
    }
}