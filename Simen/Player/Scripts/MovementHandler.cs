using Godot;
using System;

public partial class MovementHandler : Node
{
	[Export] _MovementType initialMovement;
	_MovementType currentMovement;

	public override void _Ready()
	{
		base._Ready();

		if(initialMovement != null)
			ChangeMovement(initialMovement);
	}

	public void ChangeMovement(_MovementType movement)
	{
		currentMovement?.Deactivate();
		currentMovement = movement;
		currentMovement.Activate();
	}
}
