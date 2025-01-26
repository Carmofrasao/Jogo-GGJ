using Godot;
using System;

public partial class GameHud : CanvasLayer
{
    private const int UPDATE_VALUE_DIV_FACTOR = 100;

    private int _lastScoreValue = 0;
    private int _scoreValue = 0;
    private Label _scoreLabel;
    private Label _hScoreLabel;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        this._scoreLabel = GetNode("Top").GetNode("ScoreControl").GetNode<Label>("ScoreLabel");
        this._hScoreLabel = GetNode("Top").GetNode("ScoreControl").GetNode<Label>("HScoreLabel");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
        // Text is updated in format 0000000
        this._scoreLabel.Text = this._scoreValue.ToString("D7"); 
        this._hScoreLabel.Text = this._lastScoreValue.ToString("D7");
	}

    public void Start()
    {
        this.Show();
        this._scoreValue = 0;
    }

    public void Reset()
    {
        this._lastScoreValue = Math.Max(this._lastScoreValue, this._scoreValue);
        this._scoreValue = 0;
        this.Hide();
    }

    public void UpdateScore(Vector2 playerVelocity)
    {
        // Increase score based on player velocity
        // The faster the player is moving, the more points they get
        var addValue = (int)Mathf.Abs(playerVelocity.X) + (int)Mathf.Abs(playerVelocity.Y);
        addValue /= UPDATE_VALUE_DIV_FACTOR;
        this._scoreValue += addValue;
    }
}
