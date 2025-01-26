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
        var y = Factor > 0
            ? (float)Mathf.Lerp(topLeft.Y, topLeft.Y+10, GD.Randf())
            : (float)Mathf.Lerp(bottomRight.Y-120, bottomRight.Y-130, GD.Randf());
        var windLocation = new Vector2(x: (float)Mathf.Lerp(topLeft.X, bottomRight.X, random / 16.0f), y);
        _nextWinds.Remove(random);

        var polygon = new Polygon2D
        {
            Polygon = new Vector2[] { new(0, 0), new(4, 0), new(4, 120), new(0, 120) },
            Position = windLocation,
            Color = new Color(1.0f, 1.0f, 1.0f, (float)GD.RandRange(0.3, 0.7)),
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
            velocity += ((Factor < 0 ? -250.0f : 250.0f) + Factor * 5.0f) * 2f * (float)delta * Vector2.Down.Rotated(rotation);
            ((Bubble)bubble).Velocity = velocity;
        }

        var collisionShape = GetNode<CollisionShape2D>("CollisionShape2D");
        var rectangleSize =  ((RectangleShape2D)collisionShape.Shape).Size / 2;
        var bottomRight = collisionShape.Position + rectangleSize;

        Array<Polygon2D> toRemove = new();
        foreach (var wind in _winds) {
            wind.Position += ((Factor < 0 ? -250.0f : 250.0f) + Factor * 5.0f) * Vector2.Down * (float)delta;
            if (wind.Position.Y+120 > bottomRight.Y || wind.Position.Y < 0) {
                toRemove.Add(wind);
            }
        }
        if (_winds.Count > 10) {
            toRemove.Add(_winds[0]);
        }

        foreach (var wind in toRemove) {
            wind.QueueFree();
            _winds.Remove(wind);
        }
    }

    public override void _Process(double delta)
    {
		if (Input.IsActionPressed("ui_left"))
		{
			Factor = (float)Mathf.Clamp(Factor - 100 * delta, -50, 50);
		}
		if (Input.IsActionPressed("ui_right"))
		{
			Factor = (float)Mathf.Clamp(Factor + 100 * delta, -50, 50);
		}
    }
}
