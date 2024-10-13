using Godot;
using System;

public partial class OnDeathNode : Node
{
	private Health _health;
	// Called when the node enters the scene tree for the first time.
	[Export] private string SceneName { get; set; }
	public override void _Ready()
	{
		_health = GetNode<Health>("../Health");
		_health.OnDead += () =>
		{
			GetTree().ChangeSceneToFile(SceneName);
		};
	}
}
