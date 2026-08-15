using Godot;
using System.Linq;

public partial class PickableBase : RigidBody3D
{
    public PickableVisual Visual { get; set; }

    [Export]
    public PrickableResource Resource { get; private set; }

    public override void _Ready()
    {
        Visual = GetNode<PickableVisual>(nameof(Visual));

        if (Resource.GenerateCollisionShape)
        {
            var mesh = Visual.Meshes.MaxBy(x => x.GetAabb().Size.Length());
        
            var size = mesh.GetAabb().Size * Resource.Scale;

            AddChild(new CollisionShape3D
            {
                Position = Resource.UseOffset ? mesh.Position + Vector3.Up * size.Y * 0.2f : mesh.Position,
                Shape = new BoxShape3D
                {
                    Size = size
                }
            });
        }
        else
        {
            Visual.Position = new Vector3(0, -0.53f, 0);
            AddChild(new CollisionShape3D
            {
                RotationDegrees = new Vector3(90, 0, 0),
                Shape = new CapsuleShape3D
                {
                    Radius = 0.648f * Resource.Scale,
                    Height = 1.666f * Resource.Scale
                }
            });
        }

        Visual.Scale = Vector3.One * Resource.Scale;
    }
}
