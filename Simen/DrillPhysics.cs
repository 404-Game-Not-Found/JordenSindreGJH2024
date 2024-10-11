using Godot;
using System;

public partial class DrillPhysics : Node
{
	[Export] bool isActive = true;
	
	bool isGrounded;


	[Export] float drillMaxSpeed;
	[Export] float drillAcceleration;
	float currentDrillSpeed;

	Area2D trigger;
	CollisionShape2D collider;

	public override void _Ready()
	{
		trigger = GetNode<Area2D>("%Area2D");
		collider = GetNode<CollisionShape2D>("%Collider");

		if (isActive)
			Activate();
		else
			Deactivate();

		GD.Print($"Trigger: {trigger}");
	}

	public override void _PhysicsProcess(double delta)
	{
		//GD.Print("Hello!");

	}




	public void Activate()
	{
		bool isActive = true;
		GD.Print("Activated!");
		collider.Disabled = true;

		trigger.BodyEntered += OnAreaShapeEntered;
		trigger.BodyExited += OnAreaShapeExited;
	}

	public void Deactivate()
	{
		bool isActive = false;
		collider.Disabled = false;

		trigger.BodyEntered -= OnAreaShapeEntered;
		trigger.BodyExited -= OnAreaShapeExited;
	}





	public void OnAreaShapeEntered(Node2D body)
	{
		GD.Print("Area Entered!");

		currentDrillSpeed += drillAcceleration;
		currentDrillSpeed = Mathf.Min(currentDrillSpeed, drillMaxSpeed);
	}

	public void OnAreaShapeExited(Node2D body)
	{

	}
}
