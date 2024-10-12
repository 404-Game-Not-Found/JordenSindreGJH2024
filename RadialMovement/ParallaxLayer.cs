using Godot;

public partial class ParallaxLayer : Sprite2D
{
	[Export]
	public float ParallaxRatio {private set; get;}= 0.5f;
}
