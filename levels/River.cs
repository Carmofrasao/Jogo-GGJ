using Godot;
using System;

public partial class River : Node2D, IBaseLevel
{
	private bool _oneShotTransition = true;
	
	[Export]
	public PackedScene nextScene;
	
	[Signal]
	public delegate void TransitionSceneEventHandler(PackedScene nextScene);
	
	public float TargetX { get; set; } = 0.0f;

	public float TargetY { get; set; } = -450.0f;

	public int LimitLeft { get; set; } = -1536;
	public int LimitRight { get; set; } = 1536;
	public int LimitTop { get; set; } = -3424;
	public int LimitBottom { get; set; } = 384;
	
	public void _on_win_flag(Node2D other){
		if(_oneShotTransition)
			_oneShotTransition = false;
		WinScene();
	}
	
	public void WinScene(){
		EmitSignal(SignalName.TransitionScene, nextScene);
	}
}
