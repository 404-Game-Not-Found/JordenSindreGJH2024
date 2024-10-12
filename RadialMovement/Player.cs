using Godot;
using System;

public partial class Player : CharacterBody2D
{
	public const float Speed = 300.0f;
	public const float JumpVelocity = -400.0f;

	private Camera2D _camera;
	private AnimatedSprite2D _sprite;

	public override void _Ready()
	{
		FloorMaxAngle = 45;

		_camera = GetNodeOrNull<Camera2D>("/root/Main/Camera");
		if (_camera == null) {
			GD.PrintErr("No Camera with name 'Camera' in main scene...");
			throw new MissingMemberException("Camera missing in main scene");
		}

		_sprite = GetNodeOrNull<AnimatedSprite2D>("Sprite");
		if (_sprite == null) {
			GD.PrintErr("No AnimatedSprite2D with name 'Sprite' attached to player...");
			throw new MissingMemberException("AnimatedSprite2D missing on player");
		}
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = GlobalTransform.BasisXformInv(Velocity);

		// Handle Jump.
		if (Input.IsActionJustPressed("jump") && IsOnFloor())
		{
			velocity.Y = JumpVelocity;
		}

		// Get the input direction and handle the movement/deceleration.
		Vector2 direction = Input.GetVector("move_left", "move_right", "jump", "crouch");

		if (direction != Vector2.Zero)
		{
			velocity.X = direction.X * Speed;

			// Set run or jump animation
			if (IsOnFloor()) {
				_sprite.Play("run");
			} else {
				_sprite.Play("jump");
			}
			_sprite.FlipH = velocity.X < 0;
		}
		else
		{
			velocity.X = 0;

			// Set idle animation
			_sprite.Play("idle");
		}

		// Set position and rotation of camera
		_camera.Rotate(_camera.GetAngleTo(Global.world.GlobalPosition) - Mathf.Pi/2);
		_camera.Position = Position;

		Velocity = GlobalTransform.BasisXform(velocity);
		MoveAndSlide();
	}
}
