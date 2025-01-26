using Godot;
using System;

public partial class Bird : CharacterBody2D
{
    const int DETECTION_TIME_MS = 1000;

    private bool _aware;
    private bool _attacking;
    private bool _detectingPlayer;
    private float _lastPlayerDetectionTime;
    
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        _aware = false;
        _attacking = false;
        _detectingPlayer = false;
        _lastPlayerDetectionTime = 0;
        this.Velocity = new Vector2(x: -50, y: 0);
        Play("default");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
        if (IsAttackFinished())
        {
            _attacking = false;
            this.GetNode("Area2D").Set("Disabled", true);
        }
        var collision = MoveAndCollide(Velocity * (float)delta);
        if (collision != null)
        {
            // Matei ele
            GD.Print("Matei ele");
        }

        if (!_attacking)
        { // Check if player is in detection area
            _detectingPlayer = false;
            var area = GetNode<Area2D>("Area2D");
            if (area != null)
            {
                var bodies = area.GetOverlappingBodies();
                foreach (var body in bodies)
                {
                    if (body is Bubble)
                    {
                        this._detectingPlayer = true;
                        Aware();
                        break;
                    }
                }
            }
        }

        if (_aware)
        {
            if (_detectingPlayer)
            {
                GD.Print("Player detected and aware");
                GD.Print("Time: " + Time.GetTicksMsec() + " - " + _lastPlayerDetectionTime);
                GD.Print("Diff: " + (Time.GetTicksMsec() - _lastPlayerDetectionTime));
                if (Time.GetTicksMsec() - _lastPlayerDetectionTime >= DETECTION_TIME_MS)
                {
                    GD.Print("Attack!");
                    _aware = false;
                    _detectingPlayer = false;
                    Attack();
                }
            }
            else
            {
                _aware = false;
                Play("default");
            }
        }
	}


    private void Play(string animationName)
    {
        var animPlayer = GetNode<AnimatedSprite2D>("BirdAnimation");
        if (animPlayer != null)
        {
            animPlayer.Play(animationName);
        }
    }

    private bool IsAttackFinished()
    {
        return GetNode<AnimatedSprite2D>("BirdAnimation").IsPlaying() == false;
    }

    private void Attack()
    {
        if (_attacking == false)
        {
            this._aware = false;
            this._detectingPlayer = false;
            this.GetNode("Area2D").Set("Disabled", true);
            Velocity = new Vector2(x: -400, y: 0);
            _attacking = true;
            Play("attack");
        }
    }

    private void Aware()
    {
        if (!_aware)
        {
            _lastPlayerDetectionTime = Time.GetTicksMsec();
            GD.Print("Player detected: " + _lastPlayerDetectionTime);
            Velocity = new Vector2(x: 0, y: 0);
            this._aware = true;
            Play("aware");
        }
    }
}
