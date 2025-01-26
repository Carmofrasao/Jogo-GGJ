using Godot;
using System;

public partial class BubblePlanet : Area2D
{
	[Export]
	public float Factor { get; set; } = 30.0f;

	public override void _Process(double delta)
	{
		QueueRedraw();
	}

	private const int _rate = 3000;
	private float _elapsed = 0.0f;

	public override void _Draw()
	{
		for (int i = 0; i < 4; i++)
		{
			var current = ((_elapsed + _rate*i/4.0f) % _rate + _rate) % _rate / (float)_rate;
			var color = new Color(1f, 1f, 1f, 0.5f*current);
			DrawCircle(new Vector2(0, 0), Mathf.Lerp(800.0f, 0.0f, current), color, false, 10.0f, true);
		}
		_elapsed += Factor;
	}

	public override void _PhysicsProcess(double delta)
	{
		foreach (var body in GetOverlappingBodies())
		{
			if (body is Bubble bubble)
			{
				Vector2 velocity = bubble.Velocity;
				velocity += Factor * (this.GlobalPosition - bubble.GlobalPosition).Normalized();
				bubble.Velocity = velocity;
			}
		}
	}
}
