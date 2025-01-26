using Godot;
using System;
using Godot.Collections;

public partial class Attractor : Area2D
{
    [Export]
    public float Factor { get; set; } = 50.0f;

    private Array<Polygon2D> _winds = new();

    private Array<int> _nextWinds = new();

    // Called by WindTimer
    public void WindSpawn()
    {
        var collisionShape = GetNode<CollisionShape2D>("CollisionShape2D");
        var rectangleSize =  ((RectangleShape2D)collisionShape.Shape).Size / 2;
        var topLeft = collisionShape.Position - rectangleSize;
        var bottomRight = collisionShape.Position + rectangleSize;
        if (_nextWinds.Count == 0) {
            for (int i = 0; i < 16; i++) {
                _nextWinds.Add(i);
            }
        }
        var random = _nextWinds.PickRandom();
        var windLocation = new Vector2(
            x: (float)Mathf.Lerp(topLeft.X, bottomRight.X, random / 16.0f),
            y: (float)Mathf.Lerp(topLeft.Y, topLeft.Y + 10, GD.Randf())
        );
        _nextWinds.Remove(random);

        var polygon = new Polygon2D
        {
            Polygon = new Vector2[] { new(0, 0), new(4, 0), new(4, 120), new(0, 120) },
            Position = windLocation,
        };
        _winds.Add(polygon);
        AddChild(polygon);
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

        var collisionShape = GetNode<CollisionShape2D>("CollisionShape2D");
        var rectangleSize =  ((RectangleShape2D)collisionShape.Shape).Size / 2;
        var bottomRight = collisionShape.Position + rectangleSize;

        Array<Polygon2D> toRemove = new();
        foreach (var wind in _winds) {
            wind.Position += Factor * 10.0f * Vector2.Down * (float)delta;
            if (wind.Position.Y+120 > bottomRight.Y) {
                toRemove.Add(wind);
            }
        }

        foreach(var wind in toRemove) {
                wind.QueueFree();
            _winds.Remove(wind);
        }
    }
}
