using Godot;
using System;

public partial class Explosion : Node2D
{
	[ExportGroup("Explosion Settings")]
	[Export]
	private Texture2D[] _explosionTextures;
	[Export]
	private float _explosionSpeed;

	[Export]
	private AudioStreamMP3[] _sounds;

	private AudioStreamPlayer2D _explosionAudio;

	private Sprite2D[] _explosionSprites;
	private Vector2[] _directions;

	private AnimatedSprite2D _cloudSprite;

	private bool _isExploding = false;
	
	private Godot.Timer _deleteTimer;


	public void Explode() {
		for (var i = 0; i < _explosionTextures.Length; i++) {
			var sprite = new Sprite2D();
			sprite.Texture = _explosionTextures[i];
			AddChild(sprite);

			_explosionSprites[i] = sprite;
			_directions[i] = new Vector2(GD.RandRange(-100, 100) / 100.0f, GD.RandRange(-100, 100) / 100.0f).Normalized(); 
		}

		_isExploding = true;
		_explosionAudio.Stream = _sounds[GD.Randi() % _sounds.Length];
		_explosionAudio.Play();
		_cloudSprite.Visible = true;
		_cloudSprite.Play("explode");
		GetParent().GetNode<AnimatedSprite2D>("AnimatedSprite2D").Visible = false;
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

		_explosionAudio = GetNodeOrNull<AudioStreamPlayer2D>("Sound");
		if (_explosionAudio == null) {
			GD.PrintErr("No Sound on Explosion node...");
			QueueFree();
			return;
		}

		_cloudSprite = GetNodeOrNull<AnimatedSprite2D>("Cloud");
		if (_cloudSprite == null) {
			GD.PrintErr("No Cloud on Explosion node...");
			QueueFree();
			return;
		}
		_cloudSprite.AnimationFinished += () => _cloudSprite.Visible = false;

		_deleteTimer.Timeout += GetParent().QueueFree;

		GD.Randomize();
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) {
		if (_isExploding) {
			for (var i = 0; i < _explosionSprites.Length; i++) {
				var dir = _directions[i];
				var sprite = _explosionSprites[i];
				GD.Print(_deleteTimer.TimeLeft);
				sprite.Modulate = new Color(1, 1, 1, 1.34f * (float)_deleteTimer.TimeLeft);

				sprite.Position += dir * _explosionSpeed * (float)delta;
			}
		}
	}
}
