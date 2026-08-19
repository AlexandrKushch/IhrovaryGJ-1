using Godot;

public partial class LevelManager : Node3D
{
    public int CurrentLevelIndex { get; private set; } = 1;
    private Node3D _currentLevel;

    public static LevelManager Instance { get; private set; }

    [Export]
    public PackedScene[] Levels;

    [Export]
    private PackedScene Transition;

    public override void _Ready()
    {
        if (!IsInstanceValid(Instance))
        {
            Instance = this;
        }

        SwitchToLevel(CurrentLevelIndex);
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

        if (index != 0)
        {
            var transition = Transition.Instantiate();
            transition.TreeExited += () => ChangeLevel(index);
            AddChild(transition);
        }
        else
        {
            ChangeLevel(index);
        }
    }

    private void ChangeLevel(int index)
    {
        CurrentLevelIndex = index;
        _currentLevel = Levels[CurrentLevelIndex].Instantiate<Node3D>();
        AddChild(_currentLevel);
    }
}
