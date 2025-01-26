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

    public void StartButtonPressed()
    {
        GetNode<CanvasGroup>("TitleScreen").GetNode<Button>("StartButton").Hide();
        EmitSignal(SignalName.StartGame);
    }
}
