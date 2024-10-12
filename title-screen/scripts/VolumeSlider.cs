using Godot;
using System;

public partial class VolumeSlider : HSlider
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Value = Global.soundVolume;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Global.soundVolume = Value;
	}
}
