using Godot;
using System;

public partial class Sky : Node2D, IBaseLevel
{
    public float TargetX { get; set; } = 450.0f;

    public float TargetY { get; set; } = 0.0f;
    public int LimitLeft { get; set; } = -10000000;
    public int LimitRight { get; set; } = 10000000;
    public int LimitTop { get; set; } = -572;
    public int LimitBottom { get; set; } = 1600;
}
