using Godot;
using System;
using System.Linq;

public partial class PickableVisual : Node3D
{
    private bool _selected = false;
    private bool _pickable = false;

    public MeshInstance3D[] Meshes { get; private set; }

    [Export]
    private ShaderMaterial _outlineShader;

    public override void _Ready()
    {
        var resource = GetParent<PickableBase>().Resource;
        var itemScene = resource.Items[new Random().NextInt64(resource.Items.Length)];
        var item = itemScene.Instantiate<Node3D>();
        AddChild(item);

        Meshes = item.FindChildren("*")
            .Where(x => x is MeshInstance3D mesh && mesh != null)
            .Select(x => x as MeshInstance3D)
            .ToArray();

        UpdateSelected(false);
    }

    public void UpdateSelected(bool selected)
    {
        if (_selected == selected) return;

        foreach (var mesh in Meshes)
        {
            mesh.MaterialOverlay = selected ? _outlineShader : null;
        }

        _selected = selected;
    }
}
