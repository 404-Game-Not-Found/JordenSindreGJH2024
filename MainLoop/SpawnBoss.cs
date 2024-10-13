using Godot;
using System;

public partial class SpawnBoss : Area2D
{
	public override void _Ready()
	{
		GetNode<Area2D>("../SpawnBoss").AreaEntered += other =>
		{
			if (other.IsInGroup("Drill") || other.IsInGroup("Player"))
			{
				
			}
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
