using Godot;
using System;
using System.Linq;

public partial class PickableVisual : Node3D
{
    private MeshInstance3D[] _meshes;

    [Export]
    private ShaderMaterial _outlineShader;

    public override void _Ready()
    {
        var resource = GetParent<PickableBase>().Resource;
        var itemScene = resource.Items[new Random().NextInt64(resource.Items.Length)];
        var item = itemScene.Instantiate<Node3D>();
        AddChild(item);

        _meshes = item.FindChildren("*")
            .Where(x => x is MeshInstance3D mesh && mesh != null)
            .Select(x => x as MeshInstance3D)
            .ToArray();

        UpdateSelected(false);
    }

    public void UpdateSelected(bool selected)
    {
        foreach (var mesh in _meshes)
        {
            mesh.MaterialOverlay = selected ? _outlineShader : null;
        }
    }
}
