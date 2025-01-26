using Godot;
using System;

public partial class Hud : CanvasLayer
{
	[Signal]
	public delegate void StartGameEventHandler();
	
	public void ShowMessage(string text) {
		var message = GetNode<Label>("Message");
		message.Text = text;
		message.Show();
	
		GetNode<Timer>("MessageTimer").Start();
	}
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) {
	}
	
	// This method is automatically created when you connect the signal
	private void _on_start_button_pressed() {
		Hide();  // Hide the HUD
	}
}
