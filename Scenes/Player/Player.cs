using Godot;
using System;

public partial class Player : CharacterBody3D
{
	public const float Speed = 350.0f;
	public const float GravitySpeed = 2.5f;
	public const float Deaccalaration = 15.0f;
	public const float JumpVelocity = 4.5f;

	private PlayerVisual Visual;

	private Fork Fork;

	[Export]
	public float TireTurnSpeed { get; set; } = 2.0f;

	[Export]
	public float TireMaxTurn { get; set; } = 25;

	public override void _Ready()
	{
		Visual = GetNode<PlayerVisual>(nameof(Visual));
		Fork = GetNode<Fork>(nameof(Fork));

		GetNode<ForkCollisionShapeFollower>("ForkCollisionShape").Follow = Fork.MovablePart;
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector3 velocity = Velocity;

		var accelerationInput = Input.GetAxis("ui_down", "ui_up");

		Acceleration(ref velocity, accelerationInput, (float)delta);
		SteeringRotation(velocity, accelerationInput, (float)delta);

		Velocity = velocity;
		MoveAndSlide();
	}

	private void Acceleration(ref Vector3 velocity, float accelerationInput, float delta)
	{
		if (!IsOnFloor())
		{
			velocity += GetGravity() * GravitySpeed * delta;
		}
        
        if (accelerationInput != 0)
        {
            velocity = (-GlobalBasis.Z * accelerationInput).Normalized() * Speed * delta;
        }
        else
        {
            float weight = 1f - Mathf.Exp(-Deaccalaration * (float)delta);
            velocity = velocity.Lerp(new Vector3(0, velocity.Y, 0), weight);
        }
	}

	private void SteeringRotation(Vector3 velocity, float accelerationInput, float delta)
	{
		var turnInput = Input.GetAxis("ui_left", "ui_right") * TireTurnSpeed;

		if (velocity != Vector3.Zero)
		{
			var rotation = Rotation;
			rotation.Y += (-1) * turnInput * Mathf.Sign(accelerationInput) * delta;
			Rotation = rotation;
		}

		Visual.UpdateSteeringWheels(turnInput, TireMaxTurn, TireTurnSpeed, (float)delta);
	}
}
