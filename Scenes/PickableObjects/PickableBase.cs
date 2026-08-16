using Godot;
using System.Linq;

public partial class PickableBase : RigidBody3D
{
    private AudioStreamPlayer HitAudio;

    public PickableVisual Visual { get; set; }

    [Export]
    public PrickableResource Resource { get; set; }

    public override void _Ready()
    {
        Visual = GetNode<PickableVisual>(nameof(Visual));
        HitAudio = GetNode<AudioStreamPlayer>(nameof(HitAudio));

        if (Resource.GenerateCollisionShape)
        {
            var mesh = Visual.Meshes.MaxBy(x => x.GetAabb().Size.Length());

            var size = mesh.GetAabb().Size * Resource.Scale;

            AddChild(new CollisionShape3D
            {
                Position = Resource.UseOffset ? mesh.Position + Vector3.Up * size.Y * Resource.OffsetY : mesh.Position,
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
    
	public void PushBackFrom(Vector3 direction)
	{
        ApplyImpulse(direction * 10);
	}

    private void OnBodyEntered(Node3D body)
    {
        HitAudio.Play();
    }
}
