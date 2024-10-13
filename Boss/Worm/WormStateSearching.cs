using System;
using Godot;
using LanguageExt;

namespace JordenSindreGJH2024.Boss.Worm;

public class WormStateSearching : WormState
{
	public override void EnterState(WormStateManager ctx)
	{
		ctx.Digging = true;
		switch (ctx.Direction)
		{
			case WormStateManager.WormDirection.Left:
				break;
			case WormStateManager.WormDirection.Right:
				ctx.Body.RotationDegrees += 180;
				break;
			default:
				throw new ArgumentOutOfRangeException();
		}
	}

	public override void UpdateState(WormStateManager ctx, double deltaTime)
	{
		GD.Print($"WormStateSearching  ctx: {ctx}");
		GD.Print($"WormStateSearching  Camera: {ctx.Camera}");
		GD.Print($"WormStateSearching  Player: {ctx.Camera.Player}");
		GD.Print($"WormStateSearching  Body: {ctx.Body}");
		if (ctx.Camera.Player.GlobalPosition.DistanceTo(ctx.Body.GlobalPosition) <= ctx.SenseDistance)
		{
			ctx.Target = ctx.Camera.Player.GetParent().GetNode<WormTarget>("WormTarget");
			ctx.SwitchState(ctx.Hunting);
		}
		var upDirection = (Global.world.GlobalPosition - ctx.Body.GlobalPosition).Normalized();
		var movement = new Vector2(upDirection.Y, -upDirection.X);
		
		var threshold = 150f;
		if (Global.DistanceFromGround(ctx.Body.GlobalPosition) < threshold)
		{
			movement += ctx.Direction == WormStateManager.WormDirection.Left ? upDirection : -upDirection;
		}
		
		movement *= ctx.Direction switch
		{
			WormStateManager.WormDirection.Left => ctx.Acceleration,
			WormStateManager.WormDirection.Right => -ctx.Acceleration,
			_ => throw new ArgumentOutOfRangeException()
		};

		float angle = ctx.Body.GetAngleTo(Global.world.GlobalPosition) - Mathf.Pi / 2;
		ctx.Body.Rotate(Mathf.Clamp(angle, Mathf.DegToRad(-30), Mathf.DegToRad(30)));
		GD.Print($"WORM STATE SEARCH   Angle: {Mathf.RadToDeg(angle)}");

		ctx.Body.Velocity += movement;
		if (ctx.Body.Velocity.Length() >= ctx.MaxSpeed)
			ctx.Body.Velocity = ctx.Body.Velocity.Normalized() * ctx.MaxSpeed;
		
		ctx.Body.MoveAndSlide();
	}
}
