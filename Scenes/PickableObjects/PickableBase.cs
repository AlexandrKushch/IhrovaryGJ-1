using Godot;
using System;

public partial class PickableBase : RigidBody3D
{
    public PickableVisual Visual { get; set; }

    public override void _Ready()
    {
        Visual = GetNode<PickableVisual>(nameof(Visual));
    }
}
