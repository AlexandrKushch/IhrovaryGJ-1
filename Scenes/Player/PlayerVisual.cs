using System;
using Godot;

public partial class PlayerVisual : Node3D
{
	private Node3D _parent;
    private Vector3 _previousPosition;

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
				  wheelRotation.Y + turnInput * delta,
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
}
