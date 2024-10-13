using Godot;
using System;

public partial class ScreenShake : Node
{

	float currentShakeAmount;

	Camera2D camera;
	static ScreenShake instance;

	public override void _Ready()
	{
		base._Ready();
		instance = this;

		camera = GetOwner<Camera2D>();
		if (camera is Camera cameraScript)
			cameraScript.OnCameraProcessed += OnCameraProcessed;
	}

	private void OnCameraProcessed()
	{
		if (currentShakeAmount > 0)
			ProcessShake();
	}		

	private void ProcessShake()
	{
		GD.Print("Processing shake!");


		currentShakeAmount -= 0.1f;
		currentShakeAmount = Mathf.Max(currentShakeAmount, 0);

		RandomNumberGenerator random = new RandomNumberGenerator();
		Vector2 direction = Vector2.Up.Rotated(random.RandfRange(0, 360));



		Vector2 newPosition = camera.Position + direction * currentShakeAmount;

		camera.Position = camera.Position.Lerp(newPosition, 0.5f);
	}

	static public void Shake(float amount)
	{
		instance.currentShakeAmount = Mathf.Max(amount, instance.currentShakeAmount);
		GD.Print("Started shake!");
	}
}
