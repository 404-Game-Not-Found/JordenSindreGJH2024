namespace JordenSindreGJH2024.Boss.Worm;

using Godot;
using System;
using LanguageExt;


public partial class WormStateManager : Node
{
    private WormState _currentState;
    [Export] internal int Acceleration { get; private set; } = 100;
	[Export] internal int MaxSpeed { get; private set; } = 500;
	public Option<WormTarget> Target { get; set; } = new();

	[Export] internal int SearchRange { get; private set; } = 100;
	[Export] internal int TurnRange { get; private set; } = 50;
	[Export] internal Texture2D DebugSprite;
	[Export] internal bool Debug = false;
	[Export] internal float MovementDeviation = 0.05f;
	
	private readonly WormState _hunting = new WormStateHunting();
	private readonly WormState _searching = new WormStateSearching();

	public CharacterBody2D Body { get; private set; }
	[Export] internal float MovementAmplitude { get; private set; }

	public override void _Ready()
	{
		_currentState = _searching;
		Body = GetParent<CharacterBody2D>();
		if (Body is null) throw new MissingMemberException("Expected character body 2D"); 
		_currentState.EnterState(this);
	}

	public override void _Process(double delta)
	{
		_currentState.UpdateState(this, delta);
	}

	public void SwitchState(WormState newState)
	{
		_currentState = newState;
		_currentState.EnterState(this);
	}

}
