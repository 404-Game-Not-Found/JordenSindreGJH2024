using Godot;
using System;

public partial class MovementDrill : _MovementType
{
	bool isDrilling;


	[Export] float drillMaxSpeed;
	[Export] float drillAcceleration;
	float currentDrillSpeed;

	Area2D trigger;
	CollisionShape2D collider;
	AnimatedSprite2D renderer;
	Gravity gravity;

	public override void _Ready()
	{
		base._Ready();
		trigger = GetNode<Area2D>("%GroundSensor");
		collider = GetNode<CollisionShape2D>("%Collider");
		renderer = GetNode<AnimatedSprite2D>("%PlayerRenderer");
		gravity = GetNode<Gravity>("%Gravity");
	}

	protected override void ProcessPhysics(double delta)
	{
		base.ProcessPhysics(delta);

		DrillRotate(delta);


		if (isDrilling)
		{
			ProcessDrill(delta);
		}
		else
		{

			//character.Velocity += character.GetGravity() * (float)delta;
		}
	}

	private void DrillRotate(double delta)
	{
		Vector2 mouseDirection = (character.GetGlobalMousePosition() - character.GlobalPosition);
		//character.Rotate(Mathf.Lerp(0, character.Position.AngleTo(mouseDirection), 0.2f));
		character.Rotate(Mathf.Lerp(0, character.Transform.BasisXform(Vector2.Down).AngleTo(mouseDirection),0.9f));

	}

	void ProcessDrill(double delta)
	{
		float velocityMagnitude = character.Velocity.Length();

		velocityMagnitude += drillAcceleration * ((float)delta);
		velocityMagnitude = Mathf.Min(velocityMagnitude, drillMaxSpeed);

		Vector2 downDirection = character.GlobalTransform.BasisXform(Vector2.Down).Normalized();
		Vector2 newVelocity = (downDirection).Normalized() * velocityMagnitude;


		character.Velocity = newVelocity.Lerp(character.Velocity.Normalized() * velocityMagnitude, 0.7f);

	}


	public override void Activate()
	{
		base.Activate();

		collider.Disabled = true;

		trigger.BodyEntered += OnBodyEntered;
		trigger.BodyExited += OnBodyExit;
	}

	public override void Deactivate()
	{
		base.Activate();

		collider.Disabled = false;

		trigger.BodyEntered -= OnBodyEntered;
		trigger.BodyExited -= OnBodyExit;
	}





	public void OnBodyEntered(Node2D body)
	{
		isDrilling = true;
		gravity.isActive = false;
	}

	public void OnBodyExit(Node2D body)
	{
		isDrilling = false;
		gravity.isActive = true;

	}
}
