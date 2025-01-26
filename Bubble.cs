using Godot;
using System;

public partial class Bubble : CharacterBody2D
{
    [Export]
    public float TargetX = 0f;

    [Export]
    public float TargetY = 0f;

    public override void _PhysicsProcess(double delta)
    {
        Vector2 velocity = Velocity;
        velocity.X = (float)Mathf.Lerp(velocity.X, TargetX, 0.1);
        velocity.Y = (float)Mathf.Lerp(velocity.Y, TargetY, 0.05);
        if (Mathf.Abs(velocity.Y) < 5) {
            velocity.Y = 0;
            var random = new Random();
            velocity.Y += 50.0f*(random.NextSingle() - 0.5f);
        }
        Velocity = velocity;
        MoveAndSlide();
    }
}
