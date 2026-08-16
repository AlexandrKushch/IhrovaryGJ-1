using Godot;
using System;
using System.Linq;

public partial class Level1 : Node3D
{
    private int _currentCount = 0;
    private int _maxCountObjects = 0;
    
    private PickableBase[] _objects;
    
    [Export]
    private Node3D Objects;

    [Export]
    private Control TaskLabel;
    
    public override void _Ready()
    {
        base._Ready();

        _objects = Objects.FindChildren("*").Where(x => x is PickableBase && x != null).Select(x => x as PickableBase).ToArray();
        _maxCountObjects = _objects.Count();
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
                objectOutOfBounds.QueueFree();
                _currentCount++;

                if (_currentCount == _maxCountObjects)
                {
                    GD.Print("Done");
                    LevelManager.Instance.SwitchToLevel(LevelManager.Instance.CurrentLevelIndex + 1);
                }
            }
        }
    }

    public void ShowTask()
    {
        TaskLabel.Visible = true;
    }
}
