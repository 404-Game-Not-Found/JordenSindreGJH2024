using Godot;
using System;

public partial class Health : Node
{
	int health;
	[Export] int maxHealth;


	public Action OnDead;
	/// <summary>
	/// int: New Health
	/// </summary>
	public Action<int> OnDamaged;

	public override void _Ready()
	{
		base._Ready();

		health = maxHealth;
	}

	public void DealDamage(int damage)
	{
		health-=damage;
		health = Mathf.Max(health, 0);

		OnDamaged?.Invoke(health);

		if (health <= 0)
			OnDead?.Invoke();
	}
}
