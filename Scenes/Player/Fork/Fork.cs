using Godot;
using System;

public partial class Fork : Node3D
{
    private const float MaxY = 2;
    private const float MinY = 0;

	public const float Speed = 5.0f;
	public const float Deaccalaration = 15.0f;

    private AnimatableBody3D MovablePart;

    public override void _Ready()
    {
        MovablePart = GetNode<AnimatableBody3D>(nameof(MovablePart));
    }

    public override void _PhysicsProcess(double delta)
    {
        HandleForkMovement(delta);
        // var velocity = MovablePart.Velocity;
        // var direction =  Input.GetAxis("fork_down", "fork_up");

        // if (direction < 0 && MovablePart.Position.Y > MinY
        //     || direction > 0 && MovablePart.Position.Y < MaxY)
        // {
        //     velocity.Y = direction * Speed * (float)delta;
        // }
        // else
        // {
        //     float weight = 1f - Mathf.Exp(-Deaccalaration * (float)delta);
        //     velocity.Y = Mathf.Lerp(velocity.Y, 0, weight);
        // }

        // MovablePart.Velocity = velocity;
        // MovablePart.MoveAndSlide();
    }


	private void HandleForkMovement(double delta)
	{
		var position = MovablePart.Position;
        var direction =  Input.GetAxis("fork_down", "fork_up");
		position.Y = (float)Mathf.MoveToward(MovablePart.Position.Y, MovablePart.Position.Y + direction, Speed * delta);
		position.Y = Mathf.Clamp(position.Y, MinY, MaxY);
        
		MovablePart.Position = position;
  	}
}
