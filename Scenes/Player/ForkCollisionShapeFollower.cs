using Godot;
using System;

public partial class ForkCollisionShapeFollower : CollisionShape3D
{
    public AnimatableBody3D Follow { get; set; }

    public override void _PhysicsProcess(double delta)
    {
        GlobalPosition = Follow.GlobalPosition;
        Disabled = Follow.CollisionLayer == 0;
    }

}
