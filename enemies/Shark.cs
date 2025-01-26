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

    private float _currentSpeed = 0.0f;
    private SharkDirection _direction = SharkDirection.RIGHT;

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
        if (increaseSpeed)
        {
            increaseFactor += 0.1f;
        }
        else
        {

        }
        _currentSpeed = MinSpeed + (MaxSpeed - MinSpeed) * increaseFactor;

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
