using Godot;
using System;

public partial class BossSegment : RigidBody2D
{
	private Area2D _leftTrigger;
	private Area2D _rightTrigger;
	[Export] private Texture2D _dmgSprite;
	private Sprite2D _sprite;
	public override void _Ready()
	{
		_leftTrigger = GetNode<Area2D>("TriggerLeft");
		_rightTrigger = GetNode<Area2D>("TriggerRight");
		_sprite = GetNode<Sprite2D>("Sprite");

		_leftTrigger.AreaEntered += F;
		return;

		void F(Area2D other)
		{
			if (!other.IsInGroup("Drill")) return;
			_sprite.Texture = _dmgSprite;
		}
	}
}
