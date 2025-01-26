using Godot;

public interface IBaseLevel
{
    public float TargetX { get; set; }

    public float TargetY { get; set; }

    public int LimitLeft { get; set; }

    public int LimitRight { get; set; }

    public int LimitTop { get; set; }

    public int LimitBottom { get; set; }
}
