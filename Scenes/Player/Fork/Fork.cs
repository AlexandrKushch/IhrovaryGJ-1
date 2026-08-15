using Godot;
using System.Linq;

public partial class Fork : Node3D
{
    private const float MaxY = 2;
    private float _minY = 0;

    public const float Speed = 5.0f;
    public const float Deaccalaration = 15.0f;

    private Node3D _carriage;

    public AnimatableBody3D MovablePart { get; set; }

    private AreaController SelectItemArea;
    private Area3D MinHeightShapeArea;

    public override void _Ready()
    {
        MovablePart = GetNode<AnimatableBody3D>(nameof(MovablePart));
        SelectItemArea = GetNode<AreaController>(nameof(SelectItemArea));
        MinHeightShapeArea = MovablePart.GetNode<Area3D>(nameof(MinHeightShapeArea));
    }

    public override void _PhysicsProcess(double delta)
    {
        HandleForkMovement(delta);
        HandleForkCollision();
        HandlePickUp();
        HandleDropDown();
    }

    private void HandleForkMovement(double delta)
    {
        var position = MovablePart.Position;
        var direction = Input.GetAxis("fork_down", "fork_up");

        if (direction < 0)
        {
            if (MinHeightShapeArea.GetOverlappingBodies().Count > 0)
            {
                _minY = MovablePart.Position.Y;
            }
            else
            {
                _minY = 0.2f;
            }
        }

        position.Y = (float)Mathf.MoveToward(MovablePart.Position.Y, MovablePart.Position.Y + direction, Speed * delta);
        position.Y = Mathf.Clamp(position.Y, _minY, MaxY);
        MovablePart.Position = position;
    }

    private void HandleForkCollision()
    {
        MovablePart.CollisionLayer = 2; // Player

        if (IsInstanceValid(_carriage)) return;
        if (SelectItemArea.Bodies.Count == 0) return;

        var closestItem = SelectItemArea.Bodies.MinBy(x => x.GlobalPosition.DistanceTo(MovablePart.GlobalPosition));

        if (TryRayToGround(closestItem, Vector3.Down, out var collisionPoint))
        {
            if (Mathf.IsEqualApprox(MovablePart.GlobalPosition.Y, collisionPoint.Y, 0.5))
            {
                MovablePart.CollisionLayer = 0;
            }
        }
    }

    private void HandlePickUp()
    {
        if (SelectItemArea.Bodies.Count == 0) return;

        var direction = Input.GetAxis("fork_down", "fork_up");

        if (direction <= 0) return;

        var closestItem = SelectItemArea.Bodies.MinBy(x => x.GlobalPosition.DistanceTo(MovablePart.GlobalPosition));

        if (Mathf.IsEqualApprox(closestItem.GlobalPosition.X, MovablePart.GlobalPosition.X, 0.5f)
            && Mathf.IsEqualApprox(closestItem.GlobalPosition.Z, MovablePart.GlobalPosition.Z, 0.5f)
            && TryRayToGround(closestItem, Vector3.Down, out var collisionPoint))
        {
            JointHelper.Instance.Join(MovablePart, closestItem, closestItem.GlobalPosition.DistanceTo(collisionPoint));
            _carriage = closestItem;
        }
    }

    private void HandleDropDown()
    {
        if (_carriage == null) return;

        if (Mathf.IsEqualApprox(MovablePart.Position.Y, _minY, 0.02f))
        {
            JointHelper.Instance.Unjoin(MovablePart);
            _carriage = null;
        }
    }

    private bool TryRayToGround(Node3D from, Vector3 to, out Vector3 position)
    {
        position = Vector3.Zero;

        var spaceState = GetWorld3D().DirectSpaceState;
        var query = PhysicsRayQueryParameters3D.Create(from.GlobalPosition, from.GlobalPosition + to);
        var result = spaceState.IntersectRay(query);

        if (result.Count > 0)
        {
            position = result["position"].AsVector3();
            return true;
        }

        return false;
    }
}
