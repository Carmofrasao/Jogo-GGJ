using Godot;
using System;

public partial class Main : Node2D
{
	[Export]
	public PackedScene firstScene;
	
	private bool _firstTime = true;

	private bool _inScene = false;
	private Node _loadedScene;
	private PackedScene _currentScene;

	public void StartGame()
	{
		if(_firstTime){
			_firstTime = false;
			_currentScene = firstScene;
		}
		GetNode<Hud>("HUD").HideTitleScreen();
		LoadScene(_currentScene);
	}
	
	public void LoadScene(PackedScene Scene){
		if(_inScene){
			_loadedScene.Free();
			_inScene = false;
		}
		
		_currentScene = Scene;
			
		_loadedScene = Scene.Instantiate();
		_inScene = true;
		
		Callable myCallable = new Callable(this, MethodName.LoadScene);
		_loadedScene.Connect("TransitionScene" , myCallable);
		
		IBaseLevel level = (IBaseLevel)_loadedScene;
			
		var bubble = GetNode<Bubble>("Bubble");
		var camera = bubble.GetNode<Camera2D>("Camera2D");
		bubble.Position = new Vector2(0, 0);
		bubble.TargetX = level.TargetX;
		bubble.TargetY = level.TargetY;
        bubble.DisableFriction = level.DisableFriction;
		bubble.Reset();
		bubble.Start();
		camera.LimitBottom = level.LimitBottom;
		camera.LimitTop = level.LimitTop;
		camera.LimitLeft = level.LimitLeft;
		camera.LimitRight = level.LimitRight;
		
		AddChild(_loadedScene);
	}

	public void GameOver()
	{
		var bubble = GetNode<Bubble>("Bubble");
		bubble.Burst();
	}
	async private void Reset(){
		GetNode<Hud>("Hud").ShowTitleScreen();
		if(_inScene){
			_loadedScene.Free();
			_inScene = false;
		}
		var bubble = GetNode<Bubble>("Bubble");
		bubble.Position = new Vector2(0, 0);
		bubble.TargetX = 450.0f;
		bubble.Velocity = new Vector2(0, 0);
	}
}
