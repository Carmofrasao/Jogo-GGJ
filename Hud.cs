using Godot;
using System;

public partial class Hud : CanvasLayer
{
    [Signal]
    public delegate void StartGameEventHandler();

    public void HideTitleScreen()
    {
        GetNode<CanvasGroup>("TitleScreen").Hide();
    }

    public void ShowTitleScreen()
    {
        GetNode<CanvasGroup>("TitleScreen").Show();
    }

    public void StartButtonPressed()
    {
        EmitSignal(SignalName.StartGame);
    }
}
