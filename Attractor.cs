using Godot;
using System;

public partial class Attractor : Area2D
{
    [Export]
    public float Factor { get; set; } = 50.0f;

    private PackedScene _WindScene = GD.Load<PackedScene>("res://wind.tscn");

    // Called by WindTimer
    public void WindSpawn()
    {
        var WindLocationA = GetNode<Marker2D>("WindLocationA");
        var WindLocationB = GetNode<Marker2D>("WindLocationB");

        var WindLocation = new Vector2(
            x: (float)Mathf.Lerp(WindLocationA.Position.X, WindLocationB.Position.X, GD.Randf()),
            y: (float)Mathf.Lerp(WindLocationA.Position.Y, WindLocationB.Position.Y, GD.Randf())
        );

        var wind = _WindScene.Instantiate<Sprite2D>();
        wind.Position = WindLocation;
        wind.Rotation = Rotation - 80;
        wind.Scale = new Vector2(
            x: wind.Scale.X + GD.Randf() * 10,
            y: wind.Scale.Y
        );

        if (GD.Randf() > 0.5)
        {
            wind.SetFlipH(true);
            wind.SetFlipV(false);
        }

        AddChild(wind);
    }

    public override void _PhysicsProcess(double delta)
    {
        var rotation = GetTransform().Rotation;
        if (GetParent() is PathFollow2D)
        {
            rotation += GetParent<PathFollow2D>().GetTransform().Rotation;
        }
        foreach (var bubble in GetOverlappingBodies())
        {
            Vector2 velocity = ((Bubble)bubble).Velocity;
            velocity += Factor * Vector2.Down.Rotated(rotation);
            ((Bubble)bubble).Velocity = velocity;
        }
    }
}
