using Godot;
using System;

public partial class Hurtbox : Area2D
{
	public bool isActive = true;

	[Export] Health health;

	public void TakeDamage(int amount)
	{
		if (!isActive)
			return;

		health.DealDamage(amount);
	}
}
