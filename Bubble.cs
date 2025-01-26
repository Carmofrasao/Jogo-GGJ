using Godot;
using System;
using System.Net.Http;

public partial class Bubble : CharacterBody2D
{
    [Export]
    public float TargetX = 0f;

    [Export]
    public float TargetY = 0f;

    [Signal]
    public delegate void HitEventHandler();

    public override void _PhysicsProcess(double delta)
    {
        Vector2 velocity = Velocity;
        // more = more friction
        velocity.X = (float)Mathf.Lerp(velocity.X, TargetX, 0.9*delta);
        velocity.Y = (float)Mathf.Lerp(velocity.Y, TargetY, 0.9*delta);
        if (Mathf.Abs(velocity.Y) < 5)
        {
            velocity.Y = 0;
            velocity.Y += 50.0f * (GD.Randf() - 0.5f);
        }
        Velocity = velocity;
        MoveAndSlide();
        for (int i = 0; i < GetSlideCollisionCount(); i++) {
            EmitSignal(SignalName.Hit);
        }
    }
}
