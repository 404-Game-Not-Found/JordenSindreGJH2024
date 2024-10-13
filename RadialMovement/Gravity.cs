using Godot;
// using System;

public partial class Gravity : Node
{
	public bool isActive = true;
	[Export] private CharacterBody2D _parent;
	[Export] private bool _rotate = true;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{ 
		// Ensure parent is of CharacterBody2D. If not, remove this node
		if (!GetParent().IsClass("CharacterBody2D")) {
			GD.PrintErr($"Parent ({GetParent().Name}) of {Name} is not CharacterBody2D. Freeing the behaviour node...");
			QueueFree();
			return;
		} else
		{
			if (_parent != null) return;
			_parent = GetParent<CharacterBody2D>();
		}
	}

	public override void _PhysicsProcess(double delta)
	{
		if (!isActive)
			return;

		// Get up direction. Up direction is the direction from the characted
		// to the center of the world
		var upDirection = (Global.world.GlobalPosition - _parent.GlobalPosition).Normalized();
		_parent.UpDirection = upDirection;

		_parent.Velocity += (upDirection * 980) * (float)delta;

		if (!_rotate) return;
		
		// Rotate character according to ground
		_parent.Rotate(_parent.GetAngleTo(Global.world.GlobalPosition) - Mathf.Pi/2);
	}
}
