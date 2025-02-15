using Godot;
using System;

public partial class Planet : Area2D
{
	[Export]
	public float Factor { get; set; } = 15.0f;

	public override void _Process(double delta)
	{
		if (Input.IsActionPressed("ui_up"))
		{
			Factor = (float)Mathf.Clamp(Factor - 100 * delta, -15, 15);
		}
		if (Input.IsActionPressed("ui_down"))
		{
			Factor = (float)Mathf.Clamp(Factor + 100 * delta, -15, 15);
		}
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
		_elapsed += (Factor > 0 ? 10.0f : -10.0f) + Factor;
    }

	public override void _PhysicsProcess(double delta)
	{
		foreach (var body in GetOverlappingBodies())
		{
			if (body is Bubble bubble)
			{
				Vector2 velocity = bubble.Velocity;
				velocity += ((Factor > 0 ? 10.0f : -10.0f) + Factor) * (this.GlobalPosition - bubble.GlobalPosition).Normalized();
				bubble.Velocity = velocity;
			}
		}
	}
}
