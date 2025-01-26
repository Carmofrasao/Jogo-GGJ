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
		GetNode<Bubble>("Bubble").Reset();
        _loadedScene = firstScene.Instantiate();
        AddChild(_loadedScene);
    }
	public void StartGame()
	{
		GetNode<Hud>("HUD").HideTitleScreen();
		GetNode<Bubble>("Bubble").Position = new Vector2(0, 0);
		GetNode<Bubble>("Bubble").TargetX = 100.0f;
		GetNode<Bubble>("Bubble").Reset();
		_loadedScene = firstScene.Instantiate();
		AddChild(_loadedScene);
	}

	public void GameOver()
	{
		var bubble = GetNode<Bubble>("Bubble");
		bubble.Burst();
	}
	
	private void Reset(){
		GetNode<Hud>("HUD").ShowTitleScreen();
		_loadedScene.QueueFree();
        var bubble = GetNode<Bubble>("Bubble");
        bubble.Position = new Vector2(0, 0);
        bubble.TargetX = 450.0f;
        bubble.Velocity = new Vector2(0, 0);
	}
}
