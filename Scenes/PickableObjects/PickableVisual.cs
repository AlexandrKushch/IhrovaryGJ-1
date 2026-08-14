using Godot;
using System.Linq;

public partial class PickableVisual : Node3D
{
    private MeshInstance3D[] _meshes;

    [Export]
    private ShaderMaterial _outlineShader;

    public override void _Ready()
    {
        // _outlineShader = _outlineShader.Duplicate() as ShaderMaterial;
        // GD.Print(_outlineShader.ResourceSceneUniqueId);

        _meshes = GetChildren()
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
