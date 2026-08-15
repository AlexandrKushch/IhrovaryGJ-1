using System;
using Godot;

public partial class PlayerVisual : Node3D
{
	private Node3D _parent;
    private Vector3 _previousPosition;

	[Export(PropertyHint.ArrayType)]
	private Node3D[] AllWheels;

	[Export(PropertyHint.ArrayType)]
	private Node3D[] SteeringWheels;

    public override void _Ready()
	{
		_parent = GetParent<Node3D>();
		_previousPosition = _parent.GlobalPosition;
	}

	public void UpdateSteeringWheels(float turnInput, float maxTurn, float turnSpeed, float delta)
	{
		if (turnInput != 0)
		{
			foreach (var wheel in SteeringWheels)
			{
				var wheelRotation = wheel.Rotation;
				wheelRotation.Y = Mathf.Clamp(
				  wheelRotation.Y + turnInput * 15 * delta,
				  Mathf.DegToRad(-maxTurn),
				  Mathf.DegToRad(maxTurn));
				wheel.Rotation = wheelRotation;
			}
		}
		else
		{
			foreach (var wheel in SteeringWheels)
			{
				var wheelRotation = wheel.Rotation;
				wheelRotation.Y = Mathf.MoveToward(wheelRotation.Y, 0, turnSpeed * delta);
				wheel.Rotation = wheelRotation;
			}
		}
	}

	public void SpinWheels(float direction, float delta)
	{
		foreach (var wheel in AllWheels)
		{
			wheel.Rotation = new Vector3(wheel.Rotation.X + 15 * direction * delta, wheel.Rotation.Y, wheel.Rotation.Z);
			// wheel.Rotate(Vector3.Right, 10 * direction * delta);
			// wheel.RotateX(1 * direction * delta);
		}
	}
}
