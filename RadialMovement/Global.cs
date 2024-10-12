using Godot;
// using System;

public partial class Global : Node
{
	public static StaticBody2D world;
	
	public static double soundVolume = 50;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		world = GetNodeOrNull<StaticBody2D>("/root/Main/World");
		if (world == null) {
			GD.PrintErr("Could not get World at path at '/root/Main/World'");
		}
	}
}
