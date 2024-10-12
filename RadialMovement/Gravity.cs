using Godot;
// using System;

public partial class Gravity : Node
{
	private CharacterBody2D _parent;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{ 
		// Ensure parent is of CharacterBody2D. If not, remove this node
		if (!GetParent().IsClass("CharacterBody2D")) {
			GD.PrintErr($"Parent ({GetParent().Name}) of {Name} is not CharacterBody2D. Freeing the behaviour node...");
			QueueFree();
			return;
		} else {
			_parent = GetParent<CharacterBody2D>();
		}
	}

	public override void _PhysicsProcess(double delta)
	{
		// Get up direction. Up direction is the direction from the characted
		// to the center of the world
		var upDirection = (Global.world.GlobalPosition - _parent.GlobalPosition).Normalized();
		_parent.UpDirection = upDirection;

		// Apply gravity if character is not floored
		if (!_parent.IsOnFloor())
		{
			_parent.Velocity += (upDirection * 980) * (float)delta;
		}

		// Rotate character according to ground
		_parent.Rotate(_parent.GetAngleTo(Global.world.GlobalPosition) - Mathf.Pi/2);
	}
}
