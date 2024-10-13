using Godot;
using System;

public partial class Player : CharacterBody2D
{
	public const float Speed = 300.0f;
	public const float JumpVelocity = -400.0f;

	private AnimatedSprite2D _sprite;
	private AudioStreamPlayer2D _walkAudio;

	public override void _Ready()
	{
		FloorMaxAngle = 90;

		_sprite = GetNodeOrNull<AnimatedSprite2D>("Sprite");
		if (_sprite == null) {
			GD.PrintErr("No AnimatedSprite2D with name 'Sprite' attached to player...");
		}

		_walkAudio = GetNodeOrNull<AudioStreamPlayer2D>("WalkAudio");
		if (_walkAudio == null) {
			GD.PrintErr("No AudioStreamPlayer2D with name 'WalkAudio' attached to player...");
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

		GD.Print(IsOnFloor());
		if (direction.X != 0)
		{
			velocity.X = direction.X * Speed;

			// Set run or jump animation
			if (IsOnFloor()) {
				_sprite.Play("run");
				if (!_walkAudio.Playing)
					_walkAudio.Play();
			} else {
				_sprite.Play("jump");
				_walkAudio.Stop();
			}
			_sprite.FlipH = velocity.X < 0;
		}
		else
		{
			velocity.X = 0;

			// Set idle animation
			_sprite.Play("idle");
			_walkAudio.Stop();
		}

		Velocity = GlobalTransform.BasisXform(velocity);
		MoveAndSlide();
	}
}
