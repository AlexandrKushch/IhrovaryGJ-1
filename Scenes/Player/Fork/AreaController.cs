using System.Collections.Generic;
using Godot;

public partial class AreaController : Area3D
{
    public HashSet<PickableBase> Bodies { get; private set; }

    public override void _Ready()
    {
        Bodies = new HashSet<PickableBase>();

        Connect(Area3D.SignalName.BodyEntered, new Callable(this, nameof(OnBodyEntered)));
        Connect(Area3D.SignalName.BodyExited, new Callable(this, nameof(OnBodyExited)));
    }

    public void OnBodyEntered(Node3D node)
    {
        if (node is PickableBase pickableBase)
        {
            pickableBase.Visual.UpdateSelected(true);
            Bodies.Add(pickableBase);
        }
    }

    public void OnBodyExited(Node3D node)
    {
        if (node is PickableBase pickableBase)
        {
            pickableBase.Visual.UpdateSelected(false);
            Bodies.Remove(pickableBase);
        }
    }
}
