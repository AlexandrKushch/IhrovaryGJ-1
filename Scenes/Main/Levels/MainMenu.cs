using Godot;
using System;

public partial class MainMenu : Node3D
{
    public override void _Process(double delta)
    {
        if (Input.IsActionJustReleased("ui_accept"))
        {
            LevelManager.Instance.SwitchToLevel(LevelManager.Instance.CurrentLevelIndex + 1);
        }
    }
}
