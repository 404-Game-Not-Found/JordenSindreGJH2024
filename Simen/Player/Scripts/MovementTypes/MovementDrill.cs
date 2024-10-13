using Godot;
using System;

public partial class MovementDrill : _MovementType
{
	bool isDrilling;


	[Export] float drillMaxSpeed;
	[Export] float drillAcceleration;
	float currentDrillSpeed;
	float drillControl = 1f;

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
			ProcessDrill(delta);

		if(drillControl < 1)
		{
			drillControl += 0.02f;
			if (drillControl > 1)
				drillControl = 1;
		}
	}

	private void DrillRotate(double delta)
	{
		Vector2 mouseDirection = (character.GetGlobalMousePosition() - character.GlobalPosition);
		//character.Rotate(Mathf.Lerp(0, character.Position.AngleTo(mouseDirection), 0.2f));

		float characterRotate = Mathf.Lerp(0, character.Transform.BasisXform(Vector2.Down).AngleTo(mouseDirection), 0.2f);

		character.Rotate(Mathf.Lerp(0, characterRotate, drillControl));

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

	/// <summary>
	/// 
	/// </summary>
	/// <param name="targetPosition">The global point to bounce away from</param>
	public void Bounce(Vector2 targetPosition)
	{
		Vector2 worldDirection = targetPosition - character.GlobalPosition;
		character.Velocity = character.Velocity.Reflect(worldDirection.Rotated(Mathf.DegToRad(90)).Normalized());

		character.Rotate(character.Transform.BasisXform(Vector2.Down).AngleTo(character.Velocity));
		drillControl = 0;
		ScreenShake.Shake(character.Velocity.Length() / 200);
	}



	public void OnBodyEntered(Node2D body)
	{

		GD.Print($"Area Entered!   {body.Name}");


		if (body.IsInGroup(new StringName("Solid")))
		{
			GD.Print("SOLID Entered!");

			Bounce(Global.world.GlobalPosition);
			return;
		}

		isDrilling = true;
		gravity.isActive = false;

		ScreenShake.Shake(character.Velocity.Length()/100);
	}

	public void OnBodyExit(Node2D body)
	{

		if (body.IsInGroup(new StringName("Solid")))
			return;


		GD.Print($"Owner: {GetOwner()}");
		GD.Print("Area Exit!");
		isDrilling = false;
		gravity.isActive = true;

	}
}
