using Godot;
using System;

public partial class Wind : Sprite2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		var _animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		_animationPlayer.Play("Wind");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	public void vanish(){
		QueueFree();
	}
}
