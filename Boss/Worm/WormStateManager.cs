using JordenSindreGJH2024.RadialMovement;

namespace JordenSindreGJH2024.Boss.Worm;

using Godot;
using System;
using LanguageExt;


public partial class WormStateManager : Node
{
	private WormState _currentState;
	[Export] internal int Acceleration { get; private set; } = 100;
	[Export] internal int MaxSpeed { get; private set; } = 500;

	[Export]
	internal WormDirection Direction { get; private set; }
	[Export] public WormTarget Target { get; set; }

	[Export] internal int SearchRange { get; private set; } = 100;
	[Export] internal int SenseDistance { get; private set; } = 100;
	[Export] internal int TurnRange { get; private set; } = 500;
	[Export] internal Texture2D DebugSprite;
	[Export] internal bool Debug = false;
	[Export] internal float MovementDeviation = 0.05f;
	[Export] internal int HuntingTime { get; private set; } = 20;
	[Export] internal bool Digging { get; set; } = false;
	[Export] internal RadialMovement.Camera Camera { get; set; }
	internal readonly WormState Hunting = new WormStateHunting();
	internal readonly WormState Searching = new WormStateSearching();
	internal CollisionShape2D CollisionShape2D { get; private set; }
	internal bool isAirborne { get; set; } = false;
	
	internal enum WormDirection
	{
		Left,
		Right
	}

	internal CharacterBody2D Body { get; private set; }
	[Export] internal float MovementAmplitude { get; private set; }
	private Gravity _gravity;

	public override void _Ready()
	{
		_gravity = GetParent().GetNodeOrNull<Gravity>("Gravity");
		_currentState = Searching;
		Body = GetParent().GetNode<CharacterBody2D>("Boss");
		if (Body is null) throw new MissingMemberException("Expected character body 2D"); 
		CollisionShape2D = Body.GetNode<CollisionShape2D>("CollisionShape2D");
		_currentState.EnterState(this);
	}

	public override void _Process(double delta)
	{
		isAirborne = Global.DistanceFromGround(Body.GlobalPosition) <= 10;
		_gravity.isActive = isAirborne;
		GD.Print("IsAirborne: ", isAirborne);
		_currentState.UpdateState(this, delta);
	}

	public void SwitchState(WormState newState)
	{
		switch (newState)
		{
			case WormStateHunting:
				GD.Print("Switching to hunting state");
				break;
			case WormStateSearching:
				GD.Print("Switching to searching state");
				break;
		}

		_currentState = newState;
		_currentState.EnterState(this);
	}

}
