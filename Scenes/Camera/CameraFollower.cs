using Godot;
using System;

public partial class CameraFollower : Node3D
{
    private const float Speed = 10.0f;

    [Export]
    public Node3D Follow { get; set; }

    public override void _Process(double delta)
    {
        float weight = 1f - Mathf.Exp(-Speed * (float)delta);
        GlobalPosition = GlobalPosition.Lerp(Follow.GlobalPosition, weight);
    }
}
