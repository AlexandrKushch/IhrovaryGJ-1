using Godot;

public partial class Main : Node3D
{
    [Export]
    private Control PauseMenu;

    public override void _Ready()
    {
        PauseMenu.Visible = false;
    }

    public override void _Process(double delta)
    {
        if (Input.IsActionJustReleased("ui_cancel") && LevelManager.Instance.CurrentLevelIndex > 0)
        {
            PauseMenu.Visible = !PauseMenu.Visible;
            GetTree().Paused = PauseMenu.Visible;
        }
    }
}
