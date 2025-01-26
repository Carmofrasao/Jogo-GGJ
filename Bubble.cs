using Godot;
using System;
using System.Net.Http;

public partial class Bubble : CharacterBody2D
{
	[Export]
	public float TargetX = 0f;

	[Export]
	public float TargetY = 0f;

    private bool _bursting = false;

	[Signal]
	public delegate void HitEventHandler();

	[Signal]
	public delegate void BurstAnimationFinishedEventHandler();

	public override void _Ready()
	{
		Reset();
	}
	
	public void Reset(){
		Show();
		var _animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		_animationPlayer.Play("default");
        _bursting = false;
	}

    public override void _PhysicsProcess(double delta)
    {
        Vector2 velocity = Velocity;
        // more = more friction
        velocity.X = (float)Mathf.Lerp(velocity.X, TargetX, 0.9*delta);
        velocity.Y = (float)Mathf.Lerp(velocity.Y, TargetY, 0.9*delta);
        if (Mathf.Abs(velocity.Y) < 5)
        {
            velocity.Y = 0;
            velocity.Y += 50.0f * (GD.Randf() - 0.5f);
        }
        if (_bursting) {
            velocity.X = (float)Mathf.Lerp(velocity.X, 0, 0.05*delta);
            velocity.Y = (float)Mathf.Lerp(velocity.Y, 0, 0.05*delta);
            return;
        }
        Velocity = velocity;
        MoveAndSlide();
        for (int i = 0; i < GetSlideCollisionCount(); i++) {
            EmitSignal(SignalName.Hit);
        }
    }
	
	public void Burst(){
		var _animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		_animationPlayer.Play("burst");
		// burst animation calls _FinishBurst()
        _bursting = true;
	}
	
	private void _FinishBurst(){
		Hide();
		EmitSignal(SignalName.BurstAnimationFinished);
	}
}
