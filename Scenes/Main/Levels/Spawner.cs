using Godot;
using System;
using System.Collections.Generic;

public partial class Spawner : Node3D
{
    private Random _random = new Random();

    private List<PickableBase> _items = new List<PickableBase>();

    [Export]
    private PackedScene PickableBaseScene;

    [Export]
    private PrickableResource[] Resources;

    public override void _Ready()
    {
        for (int i = 0; i < 20; i++)
        {
            SpawnRandom();   
        }
    }

    public override void _Process(double delta)
    {
        var itemsToRemove = new List<PickableBase>();

        foreach (var item in _items)
        {
            if (item.GlobalPosition.Y < -30)
            {
                itemsToRemove.Add(item);
                item.QueueFree();
            }
        }

        foreach (var item in itemsToRemove)
        {
            _items.Remove(item);
        }
    }


    public void SpawnRandom()
    {
        var resourceIndex = _random.Next(Resources.Length);
        var resource = Resources[resourceIndex];

        var item = PickableBaseScene.Instantiate<PickableBase>();
        item.Resource = resource;
        AddChild(item);

        item.Position = new Vector3(_random.Next(-10, 10), _random.Next((int)item.Position.Y - 1, (int)item.Position.Y + 3), _random.Next(-10, 10));
        item.Rotation = new Vector3((float)_random.NextDouble(), (float)_random.NextDouble(), (float)_random.NextDouble());
        _items.Add(item);
    }
}
