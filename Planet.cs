using Godot;
using System;

public partial class Planet : Area2D
{
    [Export]
    public float Factor { get; set; } = 30.0f;

    public override void _Process(double delta)
    {
        if (Input.IsActionPressed("ui_up")) {
            Factor = (float)Mathf.Clamp(Factor+100*delta, -30, 30);
        }
        if (Input.IsActionPressed("ui_down")) {
            Factor = (float)Mathf.Clamp(Factor-100*delta, -30, 30);
        }
        GD.Print(Factor);
    }

    public override void _PhysicsProcess(double delta)
    {
        foreach (var body in GetOverlappingBodies()) {
            if (body is Bubble bubble) {
                Vector2 velocity = bubble.Velocity;
                velocity += Factor * (this.GlobalPosition - bubble.GlobalPosition).Normalized();
                bubble.Velocity = velocity;
            }
        }
    }
}
