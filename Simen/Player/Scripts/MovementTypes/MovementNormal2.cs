using Godot;
using System;

public partial class MovementNormal2 : _MovementType
{
	public const float Speed = 300.0f;
	public const float JumpVelocity = -400.0f;

	private AnimatedSprite2D _sprite;

	public override void _Ready()
	{
		base._Ready();

		character.FloorMaxAngle = 45;

		_sprite = GetNodeOrNull<AnimatedSprite2D>("%PlayerRenderer");
		if (_sprite == null)
		{
			GD.PrintErr("No AnimatedSprite2D with name 'Sprite' attached to player...");
		}
	}

	protected override void ProcessPhysics(double delta)
	{
		base.ProcessPhysics(delta);

		Vector2 velocity = character.GlobalTransform.BasisXformInv(character.Velocity);

		// Handle Jump.
		if (Input.IsActionJustPressed("jump") && character.IsOnFloor())
		{
			velocity.Y = JumpVelocity;
		}

		// Get the input direction and handle the movement/deceleration.
		Vector2 direction = Input.GetVector("move_left", "move_right", "jump", "crouch");

		if (direction.X != 0)
		{
			velocity.X = direction.X * Speed;

			// Set run or jump animation
			if (character.IsOnFloor())
			{
				_sprite.Play("run");
			}
			else
			{
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

		character.Velocity = character.GlobalTransform.BasisXform(velocity);
	}
}
