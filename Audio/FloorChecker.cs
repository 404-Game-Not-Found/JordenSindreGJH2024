using Godot;
using System;

public partial class FloorChecker : Area2D
{
	private int _groundCount = 0;

	public override void _Ready() {
		BodyEntered += Entered;
		BodyExited += Exited;
	}

	public bool IsOnFloor() {
		return _groundCount > 0;
	}

	private void Entered(Node2D body) {
		if (body.IsInGroup("Ground")) {
			_groundCount += 1;
		}
	}
	
	private void Exited(Node2D body) {
		if (body.IsInGroup("Ground")) {
			_groundCount -= 1;
		}
	}
}
