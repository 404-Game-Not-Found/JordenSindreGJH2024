using System;
using Godot;

namespace JordenSindreGJH2024.title_screen;

public partial class ChangeScene : Button
{
	private Button _changeSceneButton;
	[Export] private string SceneName { get; set; }
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_changeSceneButton = GetNode<Button>($"../{Name}");
		if (_changeSceneButton == null) throw new MissingMemberException("Script can only be used on Buttons");
		_changeSceneButton.ButtonUp += () => GetTree().ChangeSceneToFile($"res://{SceneName}.tscn");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}