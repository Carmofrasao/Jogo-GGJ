using Godot;
using System;

public partial class River : Node2D, IBaseLevel
{
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
	
	public void WinScene(){
		EmitSignal(SignalName.TransitionScene, nextScene);
	}
}
