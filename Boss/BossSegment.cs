using Godot;
using System;

public partial class BossSegment : CharacterBody2D
{
	private Area2D _leftTrigger;
	private Area2D _rightTrigger;
	public override void _Ready()
	{
		_leftTrigger = GetNode<Area2D>("TriggerLeft");
		_rightTrigger = GetNode<Area2D>("TriggerRight");
		var f = (Area2D other) =>
		{
			if (!other.IsInGroup("Drill")) return;
			GD.Print("WHAT; YOU DAMAGE ME!?!?!?!?!?!?");
		};
	}
}
