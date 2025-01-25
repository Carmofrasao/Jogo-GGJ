using Godot;
using System;

public partial class Bubble : CharacterBody2D
{
    public const float Speed = 300.0f;
    public const float JumpVelocity = -400.0f;

    public override void _PhysicsProcess(double delta)
    {
        Vector2 velocity = Velocity;
        velocity += GetGravity() * (float)delta;
        Velocity = velocity;
        MoveAndSlide();
    }
}
