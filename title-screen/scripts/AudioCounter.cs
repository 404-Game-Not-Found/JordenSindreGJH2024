using Godot;
using System;

public partial class AudioCounter : Label
{

	public override void _Process(double delta)
	{
		Text = $"{Global.soundVolume}%";
	}
}
