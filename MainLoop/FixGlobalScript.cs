using Godot;
using System;

public partial class FixGlobalScript : Node
{
	public override void _Ready()
	{
		Global.world = GetNodeOrNull<StaticBody2D>("/root/Main/World");
		if (Global.world == null) {
			GD.PrintErr("Could not get World at path at '/root/Main/World'");
		}
	}
}
