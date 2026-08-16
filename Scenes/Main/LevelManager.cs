using Godot;

public partial class LevelManager : Node3D
{
    public int CurrentLevelIndex { get; private set; } = 0;
    private Node3D _currentLevel;

    public static LevelManager Instance { get; private set; }

    [Export]
    public PackedScene[] Levels;

    public override void _Ready()
    {
        if (!IsInstanceValid(Instance))
        {
            Instance = this;
        }

        SwitchToLevel(0);
    }

    public void SwitchToLevel(int index)
    {
        if (IsInstanceValid(_currentLevel))
        {
            _currentLevel.QueueFree();
        }

        if (index >= Levels.Length || index < 0)
        {
            GD.PushError("No more levels");
            return;
        }

        CurrentLevelIndex = index;
        _currentLevel = Levels[CurrentLevelIndex].Instantiate<Node3D>();
        AddChild(_currentLevel);
    }
}
