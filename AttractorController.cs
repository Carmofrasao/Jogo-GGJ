using Godot;
using System;

public partial class AttractorController : PathFollow2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(Input.IsActionPressed("key_a")){
			ProgressRatio -= (float)0.01;
		}
		
		if(Input.IsActionPressed("key_d")){
			ProgressRatio += (float)0.01;
		}
	}
}
