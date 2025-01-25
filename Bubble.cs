using Godot;
using System;

public partial class Bubble : CharacterBody2D
{
    public const float Speed = 300.0f;
    public const float JumpVelocity = -400.0f;

    public override void _PhysicsProcess(double delta)
    {
        Vector2 velocity = Velocity;
        // velocity += GetGravity() * (float)delta;
        velocity.X = (float)Mathf.Lerp(velocity.X, 160, 0.1);
        velocity.Y = (float)Mathf.Lerp(velocity.Y, 0, 0.05);
        if (Mathf.Abs(velocity.Y) < 5) {
            velocity.Y = 0;
            var random = new Random();
            velocity.Y += 50.0f*(random.NextSingle() - 0.5f);
        }
        Velocity = velocity;
        MoveAndSlide();
    }
}
