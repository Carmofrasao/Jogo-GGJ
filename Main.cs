using Godot;
using System;

public partial class Main : Node2D
{
    [Export]
    public PackedScene firstScene;

    private Node _loadedScene;

    public void StartGame()
    {
        GetNode<Hud>("HUD").HideTitleScreen();
        GetNode<Bubble>("Bubble").Position = new Vector2(0, 0);
        GetNode<Bubble>("Bubble").TargetX = 450.0f;
        _loadedScene = firstScene.Instantiate();
        AddChild(_loadedScene);
    }

    public void GameOver()
    {
        GetNode<Hud>("HUD").ShowTitleScreen();
        _loadedScene.QueueFree();
    }
}
