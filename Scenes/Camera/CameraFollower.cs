using Godot;
using System;

public partial class CameraFollower : Node3D
{
    private const float Speed = 10.0f;

    [Export]
    private Node3D Follow;

    public override void _Process(double delta)
    {
        float weight = 1f - Mathf.Exp(-Speed * (float)delta);
        GlobalPosition = GlobalPosition.Lerp(Follow.GlobalPosition, weight);
    }
}
