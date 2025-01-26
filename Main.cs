using Godot;
using System;

public partial class Main : Node2D
{
    public void StartGame()
    {
        GetNode<Hud>("HUD").HideTitleScreen();
        GetNode<Bubble>("Bubble").TargetX = 100.0f;
    }
}
