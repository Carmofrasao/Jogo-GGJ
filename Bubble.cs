using Godot;
using System;
using System.Net.Http;

public partial class Bubble : CharacterBody2D
{
	[Export]
	public float TargetX = 0f;

	[Export]
	public float TargetY = 0f;

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
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;
		velocity.X = (float)Mathf.Lerp(velocity.X, TargetX, 0.1);
		velocity.Y = (float)Mathf.Lerp(velocity.Y, TargetY, 0.05);
		if (Mathf.Abs(velocity.Y) < 5)
		{
			velocity.Y = 0;
			velocity.Y += 50.0f * (GD.Randf() - 0.5f);
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
	}
	
	private void _FinishBurst(){
		Hide();
		EmitSignal(SignalName.BurstAnimationFinished);
	}
}
