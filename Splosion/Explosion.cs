using Godot;
using System;

public partial class Explosion : Node2D
{
	[Export]
	private Texture2D[] _explosionTextures;
	[Export]
	private float _explosionSpeed;

	private Sprite2D[] _explosionSprites;
	private Vector2[] _directions;

	private bool _isExploding = false;
	
	private Godot.Timer _deleteTimer;


	public void Explode() {
		for (var i = 0; i < _explosionTextures.Length; i++) {
			var sprite = new Sprite2D();
			sprite.Texture = _explosionTextures[i];
			AddChild(sprite);

			_explosionSprites[i] = sprite;
			_directions[i] = new Vector2(GD.RandRange(-100, 100) / 100.0f, GD.RandRange(-100, 100) / 100.0f).Normalized(); 
			GD.Print(_directions[i]);
		}

		_isExploding = true;
		_deleteTimer.Start();
	}

	public override void _Ready() {
		_explosionSprites = new Sprite2D[_explosionTextures.Length];
		_directions = new Vector2[_explosionTextures.Length];

		_deleteTimer = GetNodeOrNull<Godot.Timer>("DeleteTimer");
		if (_deleteTimer == null) {
			GD.PrintErr("No DeleteTimer on Explosion node...");
			QueueFree();
			return;
		}

		_deleteTimer.Timeout += QueueFree;

		GD.Randomize();
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) {
		if (_isExploding) {
			for (var i = 0; i < _explosionSprites.Length; i++) {
				var dir = _directions[i];
				var sprite = _explosionSprites[i];

				sprite.Position += dir * _explosionSpeed * (float)delta;
			}
		}
	}
}
