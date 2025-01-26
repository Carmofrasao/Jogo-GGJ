using Godot;
using System;

public partial class Shark : CharacterBody2D
{
	[Export] public float MinSpeed = 300.0f;
	[Export] public float MaxSpeed = 300.0f;
	
	[Export] public float MinX = 320.0f;
	[Export] public float MaxX = 640.0f;
	
	enum SharkDirection {
		LEFT,
		RIGHT
	};
	
	private float _currentSpeed = 0f;
	private SharkDirection _direction = SharkDirection.LEFT;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_currentSpeed = MinSpeed;
        //_aware = false;
        //_attacking = false;
        //_detectingPlayer = false;
        //_lastPlayerDetectionTime = 0;
        //this.Velocity = new Vector2(x: -50, y: 0);
        //Play("default");
	}
	
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		bool increaseSpeed = false;
		float increaseFactor = 0.0f;
		increaseSpeed = false;
		if (increaseSpeed ) {
			increaseFactor += 0.1f;
		} else {
			
		}
		_currentSpeed = MinSpeed + (MaxSpeed-MinSpeed) * increaseFactor;
		
	if (GetTransform().X.X > MaxX) {
			_direction = SharkDirection.LEFT;
			Play("default", _direction);
		} else if (GetTransform().X.X < MinX) {
			_direction = SharkDirection.RIGHT;
			Play("default", _direction);
		}
	}
	
	public override void _PhysicsProcess(double delta)
	{
		//Vector2 position = Vector2.Down.Rotated(GetTransform().Rotation);
		Vector2 position = (_direction == SharkDirection.RIGHT) ? Vector2.Left : Vector2.Right;
		
		Vector2 velocity = _currentSpeed * position;
		//Vector2 direction = Vector2.Zero;

		Rotation = (_direction == SharkDirection.RIGHT) ? 0 : MathF.PI/2;
		Velocity = velocity;
		MoveAndSlide();
	}


    private void Play(string animationName, SharkDirection direction)
    {
        var animPlayer = GetNode<AnimatedSprite2D>("SharkAnimation");
        if (animPlayer != null)
        {
			animPlayer.Play(animationName);
			if (direction == SharkDirection.RIGHT) {
            	animPlayer.FlipH = false;
			} else {
				animPlayer.FlipH = true;
			}
        }
    }
	
	
}
