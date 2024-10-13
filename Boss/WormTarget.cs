using System;
using Godot;

namespace JordenSindreGJH2024.Boss;

public partial class WormTarget : Node
{
	public Vector2 TargetPosition { get; private set; }
	private CharacterBody2D _body;
	public override void _Ready()
	{
		_body = GetNode<CharacterBody2D>("Body");
		if (_body == null) throw new MissingMemberException("Expected CharacterBody2D");
		TargetPosition = _body.GlobalPosition;
	}
	
	public override void _Process(double delta)
	{
		// TODO: Update for drilling
		if (!_body.IsOnFloor()) return;
		TargetPosition = _body.GlobalPosition;
	}
}
