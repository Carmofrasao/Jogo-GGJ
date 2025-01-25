using Godot;
using System;

public partial class Attractor : Area2D
{
    [Export]
    public float Factor { get; set; } = 50.0f;

    public override void _PhysicsProcess(double delta)
    {
        foreach (var bubble in GetOverlappingBodies()) {
            Vector2 velocity = ((Bubble)bubble).Velocity;
            velocity += Factor * Vector2.Down.Rotated(GetTransform().Rotation);
            ((Bubble)bubble).Velocity = velocity;
        }
    }
}
