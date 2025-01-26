using Godot;
using System;

public partial class Sky : Node2D, IBaseLevel
{
	private bool _oneShotTransition = true;
	
	[Export]
	public PackedScene nextScene;
	
	[Signal]
	public delegate void TransitionSceneEventHandler(PackedScene nextScene);
	
	public float TargetX { get; set; } = 450.0f;

	public float TargetY { get; set; } = 0.0f;
	public int LimitLeft { get; set; } = -10000000;
	public int LimitRight { get; set; } = 10000000;
	public int LimitTop { get; set; } = -572;
	public int LimitBottom { get; set; } = 1600;
	
	public void _on_win_flag(Node2D other){
		if(_oneShotTransition)
			_oneShotTransition = false;
		WinScene();
	}
	
	public void WinScene(){
		EmitSignal(SignalName.TransitionScene, nextScene);

	public float Factor { get; set; } = 15.0f;

    public override void _Process(double delta)
	{
        var PreviousFactor = Factor;
        var ButtonPressed = false;

		if (Input.IsActionPressed("ui_left"))
		{
			Factor = (float)Mathf.Clamp(Factor - 100 * delta, -50, 50);
		}
		if (Input.IsActionPressed("ui_right"))
		{
			Factor = (float)Mathf.Clamp(Factor + 100 * delta, -50, 50);
		}

        if ((Factor == 15 || Factor == -15) && ButtonPressed) {
            GetNode<AudioStreamPlayer>("Nope").Play();
        }
        if (PreviousFactor != Factor) {
            if (Factor < PreviousFactor) {
                GetNode<AudioStreamPlayer>("FXMaximize").Play();
            } else {
                GetNode<AudioStreamPlayer>("FXMinimize").
                Play();
            }
        }
	}
}
