using Godot;
// using System;

public partial class Global : Node
{
	public static StaticBody2D world;
	
	public static double soundVolume = 50;
	public static double playTime = 0;
	public static bool gamePaused = false;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		world = GetNodeOrNull<StaticBody2D>("/root/Main/World");
		if (world == null) {
			GD.PrintErr("Could not get World at path at '/root/Main/World'");
		}
	}

	public static void ReSetup()
	{

	}

	public static double DistanceFromGround(Vector2 position)
	{
		var shape = world.GetNode<CollisionShape2D>("CollisionShape2D");
		var origin = shape.GlobalPosition;
		var radius = (shape.Shape.GetRect().Size.X * world.Scale.X) / 2;
		return radius - origin.DistanceTo(position);
	}
}
