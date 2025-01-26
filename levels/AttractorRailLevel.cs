using Godot;
using System;

// Esse level joga um monte de lixo na bolha 
// e vc pode usar um atractor para empurrar a bolha.
// Use A e D para controlar o atractor.

// Isso é apenas um prototipo de uma ideia.
// Futuramente, sugiro que essa ideia se torne a transição entre a fase do céu e do espaço.

public partial class AttractorRailLevel : Node2D
{
    private PackedScene _PickaxeScene = GD.Load<PackedScene>("res://objects/Pickaxe.tscn");
    private PackedScene _WaterBottleScene = GD.Load<PackedScene>("res://objects/WaterBottle.tscn");
    private PackedScene _ZxtScene = GD.Load<PackedScene>("res://objects/Zxt.tscn");

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        // +debug
        GD.Print("ready");
        Start();
        // -debug
    }

    public void Start()
    {
        GD.Print("Start");
        GetNode<Timer>("ObjectSpawnTimer").Start();
    }

    public void _on_spear_spawn_timer_timeout()
    {
        var obj = new RigidBody2D();

        var rand_value = GD.RandRange(1, 4);

        if (rand_value == 1)
        {
            obj = _PickaxeScene.Instantiate<RigidBody2D>();
        }
        else if (rand_value == 2)
        {
            obj = _WaterBottleScene.Instantiate<RigidBody2D>();
        }
        else if (rand_value == 3)
        {
            obj = _ZxtScene.Instantiate<RigidBody2D>();
        }

        var objSpawnLocation = GetNode<PathFollow2D>("Player/ObjectSpawnPath/ObjectSpawnLocation");

        objSpawnLocation.ProgressRatio = GD.Randf();

        float direction = objSpawnLocation.Rotation + Mathf.Pi / 2;
        obj.Position = objSpawnLocation.Position;
        GD.Print(objSpawnLocation.Position);

        direction += (float)GD.RandRange(-Mathf.Pi / 4, Mathf.Pi / 4);
        obj.Rotation = direction;

        var velocity = new Vector2((float)GD.RandRange(150.0, 250.0), 0);
        obj.LinearVelocity = velocity.Rotated(direction);
        obj.AngularVelocity = (float)10.5;

        AddChild(obj);
    }
}
