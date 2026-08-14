using Godot;
using System;

public partial class JointHelper : Node
{
    [Export]
    private PackedScene Joint;

    public static JointHelper Instance { get; private set; }

    public override void _Ready()
    {
        if (!IsInstanceValid(Instance))
        {
            Instance = this;
        }
    }

    public void Join(Node3D a, Node3D b, float distanceY)
    {
        if (CheckHasJoint(a))
        {
            return;
        }

        var joint = Joint.Instantiate<Generic6DofJoint3D>();
        a.AddChild(joint);

        b.GlobalPosition = a.GlobalPosition + Vector3.Up * distanceY;

        joint.Name = nameof(Joint);
        joint.NodeA = a.GetPath();
        joint.NodeB = b.GetPath();
    }

    public void Unjoin(Node3D a)
    {
        if (!CheckHasJoint(a))
        {
            return;
        }

        var joint = a.GetNode<Generic6DofJoint3D>(nameof(Joint));
        joint.QueueFree();
    }

    private bool CheckHasJoint(Node3D a)
    {
        return a.HasNode(nameof(Joint));
    }
}
