using Godot;
using System;

public partial class KeyUpDownAnimationTimer : Timer
{
	[Export]
	public int Frame = 0;

	[Export]
	public int FrameCount = 5;

	public void OnTimeout() {
		foreach (var animatedSprite in GetTree().GetNodesInGroup("AnimatedKeys")) {
			((AnimatedSprite2D)animatedSprite).Frame = Frame;
		}
		Frame = (Frame + 1) % FrameCount;
	}
}
