using Godot;
using System;

public partial class Bird : CharacterBody2D
{
	const int DETECTION_THRESHOLD_TIME_MS = 500;
	const int ATTACKING_DELAY = 500;

	enum BirdState
	{
		IDLE,
		READY_TO_ATTACK,
		ATTACKING_DELAY,
		ATTACKING,
	};

	private BirdState _state = BirdState.IDLE;

	private float _ready_to_attack_start_time = 0.0f;
	private float _attack_delay_time = 0.0f;

	private Vector2 _ready_to_attack_point;
	private Bubble _bubble_to_attack;

	public override void _Process(double delta)
	{
		switch (_state)
		{
			case BirdState.IDLE:
				Velocity = new Vector2(x: -200, y: 0);
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
			case BirdState.READY_TO_ATTACK:
				Velocity = new Vector2(x: 0, y: 0);
				
				if(_bubble_to_attack.Position.X > Position.X){
					GD.Print("A");
					StopDelayForAttack();
				} else if (Time.GetTicksMsec() - _ready_to_attack_start_time >= DETECTION_THRESHOLD_TIME_MS)
				{
					StopDelayForAttack();
				} else {
					double x = _bubble_to_attack.Position.X - Position.X;
					double y = _bubble_to_attack.Position.Y - Position.Y;
					GetNode<AnimatedSprite2D>("BirdAnimation").Rotation = (float) (Math.Atan2(y, x) + Math.PI);
				}
				break;
			case BirdState.ATTACKING_DELAY:
				// Para de rotacionar e espera um tempinho para atacar.
				if (Time.GetTicksMsec() - _ready_to_attack_start_time >= ATTACKING_DELAY)
				{
					StartAttacking();
				}
				break;
			case BirdState.ATTACKING:
				Velocity = (_ready_to_attack_point - GlobalPosition).Normalized() * 1000.0f;
				if (Time.GetTicksMsec() - _ready_to_attack_start_time >= DETECTION_THRESHOLD_TIME_MS*2)
				{
					ReturnToIdle();
				}
				break;
		}
		MoveAndCollide(Velocity * (float)delta);
	}

	private void Play(string animationName)
	{
		GetNode<AnimatedSprite2D>("BirdAnimation").Play(animationName);
	}

	private bool IsAttackFinished()
	{
		return !GetNode<AnimatedSprite2D>("BirdAnimation").IsPlaying();
	}

	private void ReturnToIdle()
	{
		_state = BirdState.IDLE;
		Play("default");
	}
	
	private void StopDelayForAttack()
	{
		_state = BirdState.ATTACKING_DELAY;
		Play("aware");
	}

	private void StartAttacking()
	{
		_attack_delay_time = Time.GetTicksMsec();
		_state = BirdState.ATTACKING;
		Play("attack");
	}

	private void BecomeReadyToAttack()
	{
		_state = BirdState.READY_TO_ATTACK;
		_ready_to_attack_start_time = Time.GetTicksMsec();
		Play("aware");
	}
}
