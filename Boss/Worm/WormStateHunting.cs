using System;
using Godot;

namespace JordenSindreGJH2024.Boss.Worm;

public class WormStateHunting : WormState
{
    public override void EnterState(WormStateManager ctx)
    {
	    
    }

    public override void UpdateState(WormStateManager ctx, double deltaTime)
    {
	    if (ctx.Target.TimeSinceUpdate >= ctx.HuntingTime)
	    {
		    ctx.SwitchState(ctx.Searching);
	    }

	    if (ctx.isAirborne)
	    {
		    ctx.Body.MoveAndSlide();
		    return;
	    }
	    
		//var movement = ctx.Body.Velocity.Normalized();

		var trgAcc = ctx.Target.TargetPosition.DirectionTo(ctx.Body.GlobalPosition).Normalized();
		Vector2 movement = ctx.Body.GlobalTransform.BasisXform(Vector2.Left).Normalized();

		movement *= ctx.Acceleration;


		Vector2 targetDirection = ctx.Body.GlobalPosition.DirectionTo(ctx.Target.GlobalPosition);
		float angle = ctx.Body.Transform.BasisXform(Vector2.Left).AngleTo(targetDirection);
		GD.Print($"WORM STATE SEARCH   Angle1: {Mathf.RadToDeg(angle)}");

		angle = Mathf.Clamp(angle, Mathf.DegToRad(-0.3f), Mathf.DegToRad(0.3f));
		GD.Print($"WORM STATE SEARCH   Angle2: {Mathf.RadToDeg(angle)}");

		ctx.Body.Rotate(angle);



		ctx.Body.Velocity += movement;
	    if (ctx.Body.Velocity.Length() >= ctx.MaxSpeed)
		    ctx.Body.Velocity = ctx.Body.Velocity.Normalized() * ctx.MaxSpeed;
        		
	    ctx.Body.MoveAndSlide();
    }
}
