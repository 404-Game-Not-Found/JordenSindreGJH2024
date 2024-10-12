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

	public override void _Ready()
	{
		base._Ready();
		trigger = GetNode<Area2D>("%Area2D");
		collider = GetNode<CollisionShape2D>("%Collider");
	}

	protected override void ProcessPhysics(double delta)
	{
		base.ProcessPhysics(delta);

		if (isDrilling)
			ProcessDrill(delta);
		else
		{
			character.Velocity += character.GetGravity() * (float)delta;
			GD.Print($"Gravity Velocity: {character.Velocity}");
		}
	}

	void ProcessDrill(double delta)
	{
		float velocityMagnitude = character.Velocity.Length();

		velocityMagnitude += drillAcceleration * ((float)delta);
		velocityMagnitude = Mathf.Min(velocityMagnitude, drillMaxSpeed);

		Vector2 mouseDirection = character.GetLocalMousePosition().Normalized();
		Vector2 newVelocity = (mouseDirection).Normalized() * velocityMagnitude;

		GD.Print($"Drill velocity: {newVelocity}  Magnitude: {newVelocity.Length()}");
		character.Velocity = newVelocity;

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
		GD.Print("Area Entered!");
		isDrilling = true;
	}

	public void OnBodyExit(Node2D body)
	{
		GD.Print("Area Exit!");
		isDrilling = false;
	}
}
