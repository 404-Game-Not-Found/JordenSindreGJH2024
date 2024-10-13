using Godot;
using System;

public partial class Death : Node
{
	private Health _health;
	private Explosion _explosion;
	public override void _Ready()
	{
		_health = GetNode<Health>("../Health");
		_explosion = GetNode<Explosion>("../Explosion");
		_health.OnDead += () =>
		{
			GD.Print("I diead");
			QueueFree();
			//_explosion.Explode();
		};
	}

}
