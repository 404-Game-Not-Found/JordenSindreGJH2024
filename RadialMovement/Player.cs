using Godot;
using System;

public partial class Player : CharacterBody2D
{
	public const float Speed = 300.0f;
	public const float JumpVelocity = -400.0f;

	private AnimatedSprite2D _sprite;
	private AudioStreamPlayer2D _walkAudio;
	private AudioStreamPlayer2D _jumpAudio;
	private AudioStreamPlayer2D _landingAudio;

	[Export]
	private AudioStreamMP3[] _jumpSounds;
	[Export]
	private AudioStreamMP3[] _landingSounds;

	private bool _inAir = false;

	public override void _Ready()
	{
		FloorMaxAngle = 45;

		_sprite = GetNodeOrNull<AnimatedSprite2D>("Sprite");
		if (_sprite == null) {
			GD.PrintErr("No AnimatedSprite2D with name 'Sprite' attached to player...");
		}

		_walkAudio = GetNodeOrNull<AudioStreamPlayer2D>("WalkAudio");
		if (_walkAudio == null) {
			GD.PrintErr("No AudioStreamPlayer2D with name 'WalkAudio' attached to player...");
		}

		_jumpAudio = GetNodeOrNull<AudioStreamPlayer2D>("JumpAudio");
		if (_jumpAudio == null) {
			GD.PrintErr("No AudioStreamPlayer2D with name 'JumpAudio' attached to player...");
		}

		_landingAudio = GetNodeOrNull<AudioStreamPlayer2D>("LandingAudio");
		if (_landingAudio == null) {
			GD.PrintErr("No AudioStreamPlayer2D with name 'LandingAudio' attached to player...");
		}

		GD.Randomize();
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = GlobalTransform.BasisXformInv(Velocity);

		// Handle Jump.
		if (Input.IsActionJustPressed("jump") && IsOnFloor())
		{
			velocity.Y = JumpVelocity;
			_jumpAudio.Stream = _jumpSounds[GD.Randi() % _jumpSounds.Length];
			_jumpAudio.Play();
			_inAir = true;
		} else if (IsOnFloor() && _inAir) {
			_inAir = false;
			_landingAudio.Stream = _landingSounds[GD.Randi() % _landingSounds.Length];
			_landingAudio.Play();
		}

		// Get the input direction and handle the movement/deceleration.
		Vector2 direction = Vector2.Zero;
		if (Input.IsActionPressed("move_left")) { direction.X -= 1; }
		if (Input.IsActionPressed("move_right")) { direction.X += 1; }


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
