using Godot;
using System.Linq;

public partial class CollectableBox : StaticBody3D
{
    private int _currentCount = 0;
    private int _maxCountObjects = 0;
    private bool _collecting = false;

    private Node3D Visual;
    private AreaController CollectArea;
    private Marker3D SnapToMarker;
    private AudioStreamPlayer CollectAudio;

    [Export]
    private Node3D Objects;

    [Export]
    private Label3D Counter;

    [Signal]
    public delegate void OnFullEventHandler();

    public override void _Ready()
    {
        Visual = GetNode<Node3D>(nameof(Visual));
        CollectArea = GetNode<AreaController>(nameof(CollectArea));
        SnapToMarker = GetNode<Marker3D>(nameof(SnapToMarker));
        CollectAudio = GetNode<AudioStreamPlayer>(nameof(CollectAudio));

        _maxCountObjects = Objects.FindChildren("*").Where(x => x is PickableBase && x != null).Select(x => x as PickableBase).Count();
        UpdateLable();
    }

    public override void _Process(double delta)
    {
        if (_collecting || CollectArea.Bodies.Count == 0) return;
        _collecting = true;
        CollectAudio.Play();
        
        var closestItem = CollectArea.Bodies.First();

        closestItem.SetProcess(false);
        closestItem.SetPhysicsProcess(false);

        closestItem.GlobalPosition = SnapToMarker.GlobalPosition;

        var visualTween = CreateTween();
        float visualDuration = 0.5f;
        visualTween.TweenProperty(Visual, "scale", Vector3.One + Vector3.One * 0.3f, visualDuration * 0.5f);
        visualTween.TweenProperty(Visual, "scale", Vector3.One, visualDuration * 0.5f);

        var itemTween = CreateTween();
        itemTween.TweenProperty(closestItem, "position", GlobalPosition, 0.25f);
        itemTween.Finished += () =>
        {
            _currentCount++;
            UpdateLable();
            closestItem.QueueFree();
            _collecting = false;

            if (_currentCount == _maxCountObjects)
            {
                EmitSignal(SignalName.OnFull);
            }
        };
    }

    private void UpdateLable()
    {
        Counter.Text = $"{_currentCount}/{_maxCountObjects}";
    }
}
