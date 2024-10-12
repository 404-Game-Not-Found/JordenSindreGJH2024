using Godot;
using System;

public partial class Player : CharacterBody2D
{
	public const float Speed = 300.0f;
	public const float JumpVelocity = -400.0f;

	private Camera2D _camera;

	public override void _Ready()
	{
		FloorMaxAngle = 90;

		_camera = GetNodeOrNull<Camera2D>("Camera");
		if (_camera == null) {
			GD.PrintErr("No camera attached to player...");
			throw new MissingMemberException("Camera missing on player");
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
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 direction = Input.GetVector("move_left", "move_right", "jump", "crouch");

		if (direction != Vector2.Zero)
		{
			velocity.X = direction.X * Speed;
		}
		else
		{
			velocity.X = 0;
		}

		_camera.Rotate(_camera.GetAngleTo(Global.world.GlobalPosition) - Mathf.Pi/2);

		Velocity = GlobalTransform.BasisXform(velocity);
		MoveAndSlide();
	}
}
