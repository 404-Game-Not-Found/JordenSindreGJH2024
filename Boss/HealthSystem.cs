using Godot;
using LanguageExt;

namespace JordenSindreGJH2024.Boss;

public partial class Node : Godot.Node
{

	private enum EntityState
	{
		Invulnerable,
		Vulnerable,
	}
	
	[Export]
	private int CurrentHealth { get; set; } = 100;
	[Export]
	private int MaxHealth { get; set; } = 100;
	private Option<RigidBody2D> _rigidBody2D;
	private Option<Timer> _invTimer;
	private EntityState _entityState = EntityState.Vulnerable;
	public override void _Ready()
	{
		_rigidBody2D = GetNode<RigidBody2D>("RigidBody2D");
		_invTimer = GetNode<Timer>("Timer");
		_events();
	}

	private void _events()
	{
		_rigidBody2D.IfSome(rb =>
		{
			rb.BodyEntered += other =>
			{
				if (_entityState == EntityState.Invulnerable) return;
				var attack = other.GetScript().As<Attack>();
				if (attack == null) return;
				CurrentHealth -= attack.Damage;
				_entityState = EntityState.Invulnerable;
				_invTimer.IfSome(t => t.Start());
			};
		});
		_invTimer.IfSome(timer => timer.Timeout += () => _entityState = EntityState.Vulnerable);
	}
}
