using Godot;
using System;

public partial class Hitbox : Area2D
{
	public bool isActive = true;

	[Export] int damage;

	public override void _Ready()
	{
		base._Ready();

		AreaEntered += OnAreaEntered;
	}

	private void OnAreaEntered(Area2D area)
	{
		if (!isActive)
			return;
		if (area is not Hurtbox hurtbox)
			return;

		hurtbox.TakeDamage(damage);
	}
}
