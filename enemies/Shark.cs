using Godot;
using System;

public partial class Shark : CharacterBody2D
{
	[Export]
	public float MinSpeed = 300.0f;
	[Export]
	public float MaxSpeed = 300.0f;

	[Export]
	public float MinX = 0.0f;
	[Export]
	public float MaxX = 720.0f;

	enum SharkDirection
	{
		LEFT,
		RIGHT
	};
	
	enum SharkState
	{
		IDLE,
		READY_TO_ATTACK,
		ATTACKING_DELAY,
		ATTACKING,
	};

	private float _currentSpeed = 0.0f;
	private float _increaseFactor = 0.0f;
	private SharkDirection _direction = SharkDirection.RIGHT;
	private SharkState _state = SharkState.IDLE;
	

	private float _ready_to_attack_start_time = 0.0f;
	private float _attack_delay_time = 0.0f;

	private Vector2 _ready_to_attack_point = Vector2.Zero;
	private Bubble _bubble_to_attack = null;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_currentSpeed = MinSpeed;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		bool increaseSpeed = false;
		float increaseFactor = 0.0f;
		increaseSpeed = false;
		
		
		switch (_state)
		{
			case SharkState.IDLE:
				foreach (var body in GetNode<Area2D>("Area2D").GetOverlappingBodies())
				{
					if (body is Bubble bubble)
					{
						BecomeReadyToAttack();
						_bubble_to_attack = bubble;
						_ready_to_attack_point = bubble.GlobalPosition;
						break;
					}
				}
				break;
			default:
				break;
		}
		
		increaseSpeed = _bubble_to_attack != null;
		
		if (increaseSpeed)
		{
			_increaseFactor += 0.2f;
		}
		else
		{
			_increaseFactor -= 0.2f;
		} 
		
		if (_increaseFactor > 1.0f)
			_increaseFactor = 1.0f;
		if (_increaseFactor < 0.0f)
			_increaseFactor = 0.0f;
		
		_currentSpeed = MinSpeed + (MaxSpeed - MinSpeed) * _increaseFactor;
		
		if (_currentSpeed > MaxSpeed)
			_currentSpeed = MaxSpeed;
		if (_currentSpeed < MinSpeed)
			_currentSpeed = MinSpeed;
		
		if (GlobalPosition.X > MaxX)
		{
			_direction = SharkDirection.LEFT;
			Play("default", _direction);
		}
		else if (GlobalPosition.X < MinX)
		{
			_direction = SharkDirection.RIGHT;
			Play("default", _direction);
		}
	}
	
	private void BecomeReadyToAttack () {
		_state = SharkState.READY_TO_ATTACK;
		_ready_to_attack_start_time = Time.GetTicksMsec();
		Play("aware", _direction);
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 position = (_direction == SharkDirection.LEFT) ? Vector2.Left : Vector2.Right;
		Vector2 velocity = _currentSpeed * position;
		Velocity = velocity;
		MoveAndSlide();
	}


	private void Play(string animationName, SharkDirection direction)
	{
		var animPlayer = GetNode<AnimatedSprite2D>("SharkAnimation");
		animPlayer.Play(animationName);
		animPlayer.FlipH = direction == SharkDirection.LEFT;
	}
}
