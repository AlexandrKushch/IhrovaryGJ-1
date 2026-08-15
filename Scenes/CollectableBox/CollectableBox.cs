using Godot;
using System.Linq;

public partial class CollectableBox : StaticBody3D
{
    private AreaController CollectArea;
    private Marker3D SnapToMarker;

    public override void _Ready()
    {
        CollectArea = GetNode<AreaController>(nameof(CollectArea));
        SnapToMarker = GetNode<Marker3D>(nameof(SnapToMarker));
    }

    public override void _Process(double delta)
    {
        if (CollectArea.Bodies.Count == 0) return;
        var closestItem = CollectArea.Bodies.First();

        closestItem.SetProcess(false);
        closestItem.SetPhysicsProcess(false);
        
        closestItem.GlobalPosition = SnapToMarker.GlobalPosition;

        var tween = CreateTween();
        tween.TweenProperty(closestItem, "position", GlobalPosition, 1.0f);
        tween.Finished += closestItem.QueueFree;
    }
}
