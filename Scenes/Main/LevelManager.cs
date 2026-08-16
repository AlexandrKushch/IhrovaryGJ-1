using Godot;

public partial class LevelManager : Node3D
{
    private int _currentLevelIndex = 1;
    private Node3D _currentLevel;

    public static LevelManager Instance { get; private set; }

    [Export]
    private PackedScene[] Levels;

    public override void _Ready()
    {
        if (!IsInstanceValid(Instance))
        {
            Instance = this;
        }

        SwitchToNextLevel();
    }


    public void SwitchToNextLevel()
    {
        if (IsInstanceValid(_currentLevel))
        {
            _currentLevel.QueueFree();
        }

        if (_currentLevelIndex >= Levels.Length)
        {
            GD.PushError("No more levels");
            return;
        }

        _currentLevel = Levels[_currentLevelIndex].Instantiate<Node3D>();
        AddChild(_currentLevel);
        _currentLevelIndex++;
    }
}
