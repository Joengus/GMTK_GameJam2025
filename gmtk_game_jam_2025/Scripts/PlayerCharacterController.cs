using Godot;
using System;
using System.Diagnostics;
using System.Threading;



public partial class PlayerCharacterController : CharacterBody3D
{
    Vector3 movement_vector = new Vector3(0, 0, 0);
    [Export] float move_speed;
    [Export] float jump_force;
    [Export] double jumpBufferTime = .175f;
    private double jumpBufferCounter = 0;
    [Export] float gravity_scale;
    [Export] float mouse_sensitivity = .002f;
    [Export] float vertical_look_limit = 70f;

    Node3D camera_pivot;
    Camera3D camera;
    Vector3 input_direction = new Vector3(0, 0, 0);

    [Export] double slidingTime;
    private double slidingCounter;
    [Export] float slidingSpeed;
    private bool isSliding = false;

    public override void _Ready()
    {
        Input.MouseMode = Input.MouseModeEnum.Captured;
        camera_pivot = GetNode<Node3D>("CameraPivot");
        camera = GetNode<Camera3D>("CameraPivot/Camera3D");
    }

    public override void _Process(double delta)
    {
        if (IsOnFloor())
        {
            if (jumpBufferCounter > 0) //jump if counter is higher than 0
            {
                jumpBufferCounter = 0;
                Velocity += new Vector3(0, jump_force, 0);
                GD.Print("Jump" + Velocity);
            }
            else if (!isSliding && Velocity.Y <= 0) //set direction
            {
                var direction = Transform.Basis * new Vector3(input_direction.X * move_speed, Velocity.Y, input_direction.Z * move_speed);
                Velocity = direction;
                GD.Print("Move" + Velocity);
            }
        }

        MoveAndSlide();
        counters(delta);
        _apply_gravity(delta);
    }



    private void _apply_gravity(double delta)
    {
        if (!IsOnFloor())
        {
            Velocity += new Vector3(0, -gravity_scale * (float)delta, 0);
        }
    }


    public override void _Input(InputEvent @event)
    {
        base._Input(@event);
        if (@event is InputEventMouseMotion mouseMotion)
        {
            if (!isSliding)
            {
                // Rotate player horizontally
                RotateY(-mouseMotion.Relative.X * mouse_sensitivity);
            }


            // Rotate camera vertically
            var camera_rotation_x = camera_pivot.Rotation.X - mouseMotion.Relative.Y * mouse_sensitivity;
            camera_rotation_x = Mathf.Clamp(camera_rotation_x, Mathf.DegToRad(-vertical_look_limit), Mathf.DegToRad(vertical_look_limit));
            camera_pivot.Rotation = new Vector3(camera_rotation_x, camera_pivot.Rotation.Y, camera_pivot.Rotation.Z);
        }
        else
        {
            if (@event.IsActionPressed("Jump"))//&& IsOnFloor())
            {
                if (isSliding)
                {
                    isSliding = false;
                    slidingCounter = 0;
                    Scale = new Vector3(1, 1, 1);
                }
                jumpBufferCounter = jumpBufferTime;
                //Velocity += new Vector3(0, jump_force, 0);
            }
            else if (@event.IsActionReleased("Jump") && Velocity.Y > 0)
            {
                Velocity = new Vector3(Velocity.X, 0, Velocity.Z);
            }
            if (@event.IsActionPressed("Left"))
            {
                input_direction.X -= 1;
            }
            else if (@event.IsActionReleased("Left"))
            {
                input_direction.X += 1;
            }
            else if (@event.IsActionPressed("Right"))
            {
                input_direction.X += 1;
            }
            else if (@event.IsActionReleased("Right"))
            {
                input_direction.X -= 1;
            }
            else if (@event.IsActionPressed("Forward"))
            {
                input_direction.Z -= 1;
            }
            else if (@event.IsActionReleased("Forward"))
            {
                input_direction.Z += 1;
            }
            else if (@event.IsActionPressed("Backward"))
            {
                input_direction.Z += 1;
            }
            else if (@event.IsActionReleased("Backward"))
            {
                input_direction.Z -= 1;
            }
            else if (@event.IsActionPressed("Slide") && IsOnFloor() && !isSliding)
            {
                isSliding = true;
                slidingCounter = slidingTime;
                Scale = new Vector3(1, .5f, 1);
                Velocity = Transform.Basis * new Vector3(0, 0, -slidingSpeed);
            }
        }
    }

    private void counters(double delta)
    {
        if (isSliding && slidingCounter > 0f)
        {
            slidingCounter -= delta;
            if (slidingCounter <= 0)
            {
                isSliding = false;
                Scale = new Vector3(1, 1, 1);
            }
        }
        if (jumpBufferCounter > 0)
        {
            jumpBufferCounter -= delta;
        }
    }
}
