using System;
using Godot;

namespace JordenSindreGJH2024.Boss.Worm;

public class WormStateSearching : WormState
{
    internal enum WormDirection
    {
        Left,
        Right
    }
    
    private float _time;
    
    public override void EnterState(WormStateManager ctx)
    {
        ctx.Digging = true;
        //ctx.CollisionShape2D.Disabled = true;
        switch (ctx.Direction)
        {
            case WormDirection.Left:
                break;
            case WormDirection.Right:
                ctx.Body.RotationDegrees += 180;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public override void UpdateState(WormStateManager ctx, double deltaTime)
    {
        var upDirection = (Global.world.GlobalPosition - ctx.Body.GlobalPosition).Normalized();
        var movement = new Vector2(upDirection.Y, -upDirection.X);
        
        var threshold = 150f;
        if (Global.DistanceFromGround(ctx.Body.GlobalPosition) < threshold)
        {
            movement += ctx.Direction == WormDirection.Left ? upDirection : -upDirection;
        }
        
        movement *= ctx.Direction switch
        {
            WormDirection.Left => ctx.Acceleration,
            WormDirection.Right => -ctx.Acceleration,
            _ => throw new ArgumentOutOfRangeException()
        };


		ctx.Body.Rotate(ctx.Body.GetAngleTo(Global.world.GlobalPosition) - Mathf.Pi / 2);

        ctx.Body.Velocity += movement;
        if (ctx.Body.Velocity.Length() >= ctx.MaxSpeed)
            ctx.Body.Velocity = ctx.Body.Velocity.Normalized() * ctx.MaxSpeed;
        
        ctx.Body.MoveAndSlide();
    }
}
