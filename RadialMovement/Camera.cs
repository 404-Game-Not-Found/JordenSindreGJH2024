using System;
using Godot;
using System;

namespace JordenSindreGJH2024.RadialMovement;

public partial class Camera : Camera2D
{
	[Export]
	private float _cameraFollowSpeed = 1;

	public CharacterBody2D Player { get; private set; }

	public Action OnCameraProcessed;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{

		Player = GetNodeOrNull("../PlayerNode").GetNodeOrNull<CharacterBody2D>("WalkingPlayer");
		if (Player == null) {
			GD.PrintErr("Camera could not find player...");
		}
	}

	public void FollowPlayer(CharacterBody2D player)
	{
		Player = player;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		Rotate(GetAngleTo(Global.world.GlobalPosition) - Mathf.Pi/2);
		Position = Position.Lerp(Player.Position, _cameraFollowSpeed);
		OnCameraProcessed?.Invoke();
	}
}
