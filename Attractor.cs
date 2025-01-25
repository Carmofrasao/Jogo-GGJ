using Godot;
using System;

public partial class Attractor : Area2D
{
    [Export]
    public float Factor { get; set; } = 50.0f;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }

    public override void _PhysicsProcess(double delta)
    {
        foreach (var bubble in GetOverlappingBodies()) {
            Vector2 velocity = ((Bubble)bubble).Velocity;
            velocity += Factor * Vector2.Down.Rotated(GetTransform().Rotation);
            ((Bubble)bubble).Velocity = velocity;
        }
    }
}
