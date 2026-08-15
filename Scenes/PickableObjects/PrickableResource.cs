using Godot;

[GlobalClass]
public partial class PrickableResource : Resource
{
    [Export]
    public PackedScene[] Items { get; set; }

    [Export]
    public bool GenerateCollisionShape { get; set; }

    [Export]
    public float Scale { get; set; } = 1.0f;

    [Export]
    public bool UseOffset { get; set; } = false;
}
