using Godot;
using System;

public partial class ParallaxManager : Node2D
{
	private Camera2D _camera;
	private float _previousCamRotation;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_camera = GetNodeOrNull<Camera2D>("../Camera");
		if (_camera == null) {
			GD.PrintErr("Parallax could not find camera named 'Camera' in main scene...");
			QueueFree();
			return;
		}
	
		_previousCamRotation = _camera.Rotation;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		foreach(ParallaxLayer layer in GetChildren()) {
			layer.Rotate((_camera.Rotation - _previousCamRotation) * layer.ParallaxRatio);
		}

		_previousCamRotation = _camera.Rotation;
	}
}
