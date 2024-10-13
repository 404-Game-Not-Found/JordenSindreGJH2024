using Godot;
using System;

public partial class Timer : Label
{
	public override void _Process(double delta)
	{
		int minutes = (int)(Global.playTime / 60);
		double seconds = Global.playTime % 60;
		Text = $"{minutes}:{seconds}";
	}
}
