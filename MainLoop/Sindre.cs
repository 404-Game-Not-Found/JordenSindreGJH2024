using Godot;
using System;
using JordenSindreGJH2024.RadialMovement;
using JordenSindreGJH2024.Boss;

public partial class Sindre : Node
{
	public CharacterBody2D WalkingCharacter { get; private set; }
	public CharacterBody2D DrillingCharacter { get; private set; }
	[Export] public SindreMode _mode = SindreMode.Walking;
	public enum SindreMode { Walking, Drilling }
	private AnimatedSprite2D DrillPlayerSprite { get; set; }
	private Camera Cam { get; set; }
	Node2D wormTarget;
	public override void _Ready()
	{
		WalkingCharacter = GetNode<CharacterBody2D>("WalkingPlayer");
		DrillingCharacter = GetNode<CharacterBody2D>("DrillingPlayer");
		wormTarget = GetNode<Node2D>("WormTarget");
		if (DrillingCharacter == null) throw new MissingMemberException("Missing drilling player");
		if (WalkingCharacter == null) throw new MissingMemberException("Missing walking player");
		DrillPlayerSprite = GetNode<AnimatedSprite2D>("DrillingPlayer/DrillingPlayerSprite");
		ToggleSindreMode();
	}

	private void ToggleSindreMode()
	{
		switch (_mode)
		{
			case SindreMode.Walking:
				DrillingCharacter.ProcessMode = ProcessModeEnum.Disabled;
				DrillingCharacter.Visible = false;
				WalkingCharacter.ProcessMode = ProcessModeEnum.Always;
				WalkingCharacter.Visible = true;
				DrillPlayerSprite.Visible = false;
				break;
			case SindreMode.Drilling:
				DrillingCharacter.ProcessMode = ProcessModeEnum.Always;
				DrillingCharacter.Visible = true;
				WalkingCharacter.ProcessMode = ProcessModeEnum.Disabled;
				WalkingCharacter.Visible = false;
				DrillPlayerSprite.Visible = true;
				break;
			default:
				throw new ArgumentOutOfRangeException();
		}

		Cam?.FollowPlayer(ActiveBody(Cam));
	}

	public CharacterBody2D ActiveBody(Camera camera)
	{
		Cam = camera;
		return _mode switch
		{
			SindreMode.Walking => WalkingCharacter,
			SindreMode.Drilling => DrillingCharacter,
			_ => throw new ArgumentOutOfRangeException()
		};
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		switch (_mode)
		{
			case SindreMode.Walking:
				DrillingCharacter.GlobalPosition = WalkingCharacter.GlobalPosition;
				break;
			case SindreMode.Drilling:
				WalkingCharacter.GlobalPosition = DrillingCharacter.GlobalPosition;
				break;
			default:
				throw new ArgumentOutOfRangeException();
		}

		wormTarget.GlobalPosition = DrillingCharacter.GlobalPosition;

		if (!Input.IsActionJustReleased("toggle-sindre-mode")) return;
		if (Global.DistanceFromGround(DrillingCharacter.GlobalPosition) >= 25 && _mode == SindreMode.Drilling) return;
		_mode = _mode == SindreMode.Walking ? SindreMode.Drilling : SindreMode.Walking;
		ToggleSindreMode();
	}
}
