using Godot;
using System;
using System.Net.Http;

public partial class Bubble : CharacterBody2D
{
    private bool _updateScore = false;

    private const int UPDATE_SCORE_INTERVAL_MS = 100;

    private float _updateScoreTimer = 0;
    
	[Export]
	public float TargetX = 0f;

	[Export]
	public float TargetY = 0f;

    private bool _bursting = false;

	[Signal]
	public delegate void HitEventHandler();

	[Signal]
	public delegate void BurstAnimationFinishedEventHandler();

    [Signal]
    public delegate void UpdateScoreEventHandler(Vector2 playerVelocity);

	public override void _Ready()
	{
		Reset();
	}
	
	public void Reset(){
		Show();
        this._updateScoreTimer = Time.GetTicksMsec();
        this.Velocity = new Vector2(0, 0);
		var _animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		_animationPlayer.Play("default");
        _bursting = false;
	}

    public void Start()
    {
        this.GetNode("Camera2D").GetNode("GAMEHUD").Call("Start");
        this._updateScoreTimer = Time.GetTicksMsec();
    }

    public override void _PhysicsProcess(double delta)
    {
        Vector2 velocity = Velocity;
        var timer = Time.GetTicksMsec();

        if (timer - this._updateScoreTimer > UPDATE_SCORE_INTERVAL_MS) {
            this._updateScore = true;
        }
        if (_updateScore) {
            EmitSignal(SignalName.UpdateScore, velocity);
            this._updateScore = false;
            this._updateScoreTimer = timer;
        }

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
