using Godot;
using System;

public partial class PauseMenuControl : Control
{
    [Export]
    private Button PrevButton;

    [Export]
    private Button NextButton;

    public override void _Process(double delta)
    {
        NextButton.Disabled = LevelManager.Instance.CurrentLevelIndex + 1 >= LevelManager.Instance.Levels.Length - 1;
        PrevButton.Disabled = LevelManager.Instance.CurrentLevelIndex - 1 < 1;
    }

    private void OnContinue()
    {
        GetTree().Paused = false;
        Hide();
    }

    private void OnNextLevel()
    {
        LevelManager.Instance.SwitchToLevel(LevelManager.Instance.CurrentLevelIndex + 1);
        OnContinue();
    }    

    private void OnPrevLevel()
    {
        LevelManager.Instance.SwitchToLevel(LevelManager.Instance.CurrentLevelIndex - 1);
        OnContinue();
    }

    private void OnExit()
    {
		GetTree().Quit();
    }
}
