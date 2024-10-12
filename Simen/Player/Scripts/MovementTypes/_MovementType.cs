using Godot;
using System;

public partial class _MovementType : Node
{
	[Export] public bool isActive;

	protected CharacterBody2D character;

	public override void _Ready()
	{
		base._Ready();

		character = GetOwner<CharacterBody2D>();
	}

	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);
		if (!isActive)
			return;

		ProcessPhysics(delta);
		LateProcessPhysics(delta);
	}

	protected virtual void ProcessPhysics(double delta)
	{

	}

	protected virtual void LateProcessPhysics(double delta)
	{
		character.MoveAndSlide();
	}



	public virtual void Activate()
	{
		isActive = true;
		GD.Print($"{Name} Activated! yes");
	}

	public virtual void Deactivate()
	{
		isActive = false;
	}
}
