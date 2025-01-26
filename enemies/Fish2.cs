using Godot;
using System;

public partial class Fish2 : CharacterBody2D
{
	[Export]
	public float MinSpeed = 150.0f;
	[Export]
	public float MaxSpeed = 150.0f;

	[Export]
	public float MinX = -200.0f;
	[Export]
	public float MaxX = 720.0f;

	enum FishDirection
	{
		LEFT,
		RIGHT
	};
	
	enum FishState
	{
		IDLE,
		READY_TO_ATTACK,
		ATTACKING_DELAY,
		ATTACKING,
	};

	private float _currentSpeed = 150.0f;
	private float _increaseFactor = 0.0f;
	private FishDirection _direction = FishDirection.RIGHT;
	private FishState _state = FishState.IDLE;
	

	private float _ready_to_attack_start_time = 0.0f;
	private float _attack_delay_time = 0.0f;

	private Vector2 _ready_to_attack_point = Vector2.Zero;
	private Bubble _bubble_to_attack = null;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_currentSpeed = MinSpeed;
		Play("default", _direction);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		bool increaseSpeed = false;
		float increaseFactor;
		increaseSpeed = false;
		
		_currentSpeed = Math.Clamp(_currentSpeed, MinSpeed, MaxSpeed);
		
		if (GlobalPosition.X > MaxX)
		{
			_direction = FishDirection.LEFT;
			Play("default", _direction);
		}
		else if (GlobalPosition.X < MinX)
		{
			_direction = FishDirection.RIGHT;
			Play("default", _direction);
		}
	}
	
	private void BecomeReadyToAttack () {
		_state = FishState.READY_TO_ATTACK;
		_ready_to_attack_start_time = Time.GetTicksMsec();
		//Play("aware", _direction);
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 position = (_direction == FishDirection.LEFT) ? Vector2.Left : Vector2.Right;
		Vector2 velocity = _currentSpeed * position;
		Velocity = velocity;
		MoveAndSlide();
	}


	private void Play(string animationName, FishDirection direction)
	{
		var animPlayer = GetNode<AnimatedSprite2D>("FishAnimation");
		animPlayer.Play(animationName);
		//animPlayer.FlipH = false;
		animPlayer.FlipV = false;
		animPlayer.FlipH = direction == FishDirection.LEFT;
	}
}
