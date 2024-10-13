using System;
using Godot;

namespace JordenSindreGJH2024.Boss;

public partial class WormTarget : Node2D
{
	public Vector2 TargetPosition { get; private set; }
	[Export] public double TimeSinceUpdate { get; private set; }
	private CharacterBody2D _body;
	Sindre sindre;
	public override void _Ready()
	{
		sindre = GetParent<Sindre>();
		_body = GetNode<CharacterBody2D>("../WalkingPlayer");
		if (_body == null) throw new MissingMemberException("Expected CharacterBody2D");
		TargetPosition = _body.GlobalPosition;
	}
	
	public override void _Process(double delta)
	{
		// TODO: Update for drilling
		if (_body.IsOnFloor() || (sindre._mode == Sindre.SindreMode.Drilling && Global.DistanceFromGround((sindre._mode switch
		{
			Sindre.SindreMode.Walking => sindre.WalkingCharacter,
			Sindre.SindreMode.Drilling => sindre.DrillingCharacter,
			_ => throw new ArgumentOutOfRangeException()
		}).GlobalPosition) >= 10))
		{
			TargetPosition = _body.GlobalPosition;
			TimeSinceUpdate = 0;
		}
		else
		{
			TimeSinceUpdate += delta;
		}
	}
}
