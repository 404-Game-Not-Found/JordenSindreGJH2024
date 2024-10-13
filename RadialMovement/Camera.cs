using Godot;

namespace JordenSindreGJH2024.RadialMovement;

public partial class Camera : Camera2D
{
	[Export]
	private float _cameraFollowSpeed = 1;

	private CharacterBody2D _player;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_player = GetNodeOrNull<Sindre>("../PlayerNode").ActiveBody(this);
		if (_player == null) {
			GD.PrintErr("Camera could not find player...");
		}
	}

	public void FollowPlayer(CharacterBody2D player)
	{
		_player = player;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		Rotate(GetAngleTo(Global.world.GlobalPosition) - Mathf.Pi/2);
		Position = Position.Lerp(_player.Position, _cameraFollowSpeed);
	}
}