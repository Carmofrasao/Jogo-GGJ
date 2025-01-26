using Godot;
using System;

public partial class Space : Node2D, IBaseLevel
{
	[Export]
	public PackedScene nextScene;
	
	[Signal]
	public delegate void TransitionSceneEventHandler(PackedScene nextScene);
	
	public float TargetX { get; set; } = 450.0f;

	public float TargetY { get; set; } = 0.0f;
	
	public int LimitLeft { get; set; } = 0;
	public int LimitRight { get; set; } = 10000000;
	public int LimitTop { get; set; } = -950;
	public int LimitBottom { get; set; } = 1024;
	
	public void WinScene(){
		EmitSignal(SignalName.TransitionScene, nextScene);
	}

	public float Factor { get; set; } = 15.0f;
    
    public override void _Process(double delta)
	{
        var PreviousFactor = Factor;
        var ButtonPressed = false;
		if (Input.IsActionPressed("ui_up"))
		{
            ButtonPressed = true;
			Factor = (float)Mathf.Clamp(Factor - 100 * delta, -15, 15);
		}
		if (Input.IsActionPressed("ui_down"))
		{
            ButtonPressed = true;
			Factor = (float)Mathf.Clamp(Factor + 100 * delta, -15, 15);
		}

        if ((Factor == 15 || Factor == -15) && ButtonPressed) {
            GetNode<AudioStreamPlayer>("Nope").Play();
        }
        if (PreviousFactor != Factor) {
            if (Factor < PreviousFactor) {
                GetNode<AudioStreamPlayer>("FXMaximize").Play();
            } else {
                GetNode<AudioStreamPlayer>("FXMinimize").Play();
            }
        }
	}
}
