using Godot;
using System;

public partial class PickableBase : RigidBody3D
{
    public PickableVisual Visual { get; set; }

    [Export]
    public PrickableResource Resource { get; private set; }

    public override void _Ready()
    {
        Visual = GetNode<PickableVisual>(nameof(Visual));
    }
}
