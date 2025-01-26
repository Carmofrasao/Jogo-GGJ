using Godot;
using System;

public partial class Space : Node2D, IBaseLevel
{
    public float TargetX { get; set; } = 450.0f;

    public float TargetY { get; set; } = 0.0f;
    
    public int LimitLeft { get; set; } = 0;
    public int LimitRight { get; set; } = 10000000;
    public int LimitTop { get; set; } = -950;
    public int LimitBottom { get; set; } = 1024;
}
