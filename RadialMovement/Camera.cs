using Godot;
using System;

public partial class Camera : Camera2D
{
	private CharacterBody2D _player;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_player = GetNodeOrNull<CharacterBody2D>("../Player");
		if (_player == null) {
			GD.PrintErr("Camera could not find player...");
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Rotate(GetAngleTo(Global.world.GlobalPosition) - Mathf.Pi/2);
		Position = _player.Position;
	}
}
