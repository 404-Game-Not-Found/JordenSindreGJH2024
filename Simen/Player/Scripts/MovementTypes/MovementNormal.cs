using Godot;
using System;

public partial class MovementNormal : _MovementType
{
	public const float Speed = 300.0f;
	public const float JumpVelocity = -400.0f;

	CharacterBody2D character;

	public override void _Ready()
	{
		base._Ready();

		character = GetOwner<CharacterBody2D>();
	}

	protected override void ProcessPhysics(double delta)
	{
		base.ProcessPhysics(delta);

		Vector2 velocity = character.Velocity;

		// Add the gravity.
		if (!character.IsOnFloor())
		{
			velocity += character.GetGravity() * (float)delta;
		}

		// Handle Jump.
		if (Input.IsActionJustPressed("ui_accept") && character.IsOnFloor())
		{
			velocity.Y = JumpVelocity;
		}

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		if (direction != Vector2.Zero)
		{
			velocity.X = direction.X * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(character.Velocity.X, 0, Speed);
		}

		character.Velocity = velocity;
	}
}
