using Godot;
using System;

public partial class AttractorController : PathFollow2D
{
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
