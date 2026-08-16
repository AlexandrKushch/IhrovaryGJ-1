using Godot;
using System;
using System.Linq;

public partial class Level2 : Node3D
{
    private Random _random = new Random();
    private PickableBase[] _objects;

    private int _currentBoxCompleteCount = 0;

    [Export]
    private int BoxCount;

    [Export]
    private Control TaskLabel;
    
    [Export]
    private Node3D Objects;

    public override void _Ready()
    {
        base._Ready();

        _objects = Objects.FindChildren("*").Where(x => x is PickableBase && x != null).Select(x => x as PickableBase).ToArray();
    }

    public override void _Process(double delta)
    {
        if (Input.IsActionJustReleased("ui_cancel"))
        {
            TaskLabel.Visible = !TaskLabel.Visible;
        }

        var objectsOutOfBounds = _objects.Where(x => IsInstanceValid(x) && x.GlobalPosition.Y <= -3).ToArray();

        if (objectsOutOfBounds != null
            && objectsOutOfBounds.Length > 0)
        {
            foreach (var objectOutOfBounds in objectsOutOfBounds)
            {
                var x = _random.Next(-5, 5);
                x = x == 0 ? 1 : x;

                var z = _random.Next(-5, 5);
                z = z == 0 ? 1 : z;
                objectOutOfBounds.GlobalPosition = new Vector3(x, 2, z);
            }
        }
    }

    private void OnBoxComplete()
    {
        _currentBoxCompleteCount++;

        if (_currentBoxCompleteCount == BoxCount)
        {
            GD.Print("DOne");
            LevelManager.Instance.SwitchToLevel(LevelManager.Instance.CurrentLevelIndex + 1);
        }
    }
}
