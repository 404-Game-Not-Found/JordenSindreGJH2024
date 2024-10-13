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
	    
		var movement = ctx.Body.Velocity.Normalized();

		var trgAcc = ctx.Target.TargetPosition.DirectionTo(ctx.Body.GlobalPosition).Normalized();
		movement += trgAcc;
	    
	    movement.Normalized();
	    movement *= ctx.Direction switch
	    {
		    WormStateManager.WormDirection.Left => ctx.Acceleration,
		    WormStateManager.WormDirection.Right => -ctx.Acceleration,
		    _ => throw new ArgumentOutOfRangeException()
	    };
	    movement *= 10;
        
	    ctx.Body.Rotate(Mathf.Clamp(ctx.Body.GetAngleTo(ctx.Target.TargetPosition) + Mathf.Pi, -Mathf.Pi / 6, Mathf.Pi / 6));
        
	    ctx.Body.Velocity += movement;
	    if (ctx.Body.Velocity.Length() >= ctx.MaxSpeed)
		    ctx.Body.Velocity = ctx.Body.Velocity.Normalized() * ctx.MaxSpeed;
        		
	    ctx.Body.MoveAndSlide();
    }
}
