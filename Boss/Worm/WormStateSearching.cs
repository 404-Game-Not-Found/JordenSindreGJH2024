using System;
using Godot;

namespace JordenSindreGJH2024.Boss.Worm;

public class WormStateSearching : WormState
{
    private enum WormDirection
    {
        Left,
        Right
    }
    
    private Vector2 _startPosition;
    private Vector2 _leftPosition;
    private Vector2 _rightPosition;
    private float _time;
    
    private WormDirection _direction;
    public override void EnterState(WormStateManager ctx)
    {
        _startPosition = ctx.Body.GlobalPosition;
        _leftPosition = _startPosition - new Vector2(ctx.SearchRange, 0);
        _rightPosition = _startPosition + new Vector2(ctx.SearchRange, 0);
        _direction = new[] {WormDirection.Left, WormDirection.Right}[new RandomNumberGenerator().RandiRange(0, 1)];
        CreateGotoPositions(ctx);
    }

    public override void UpdateState(WormStateManager ctx, double deltaTime)
    {
        var movement = _direction switch
        {
            WormDirection.Left => new Vector2(ctx.Acceleration, 0),
            WormDirection.Right => new Vector2(-ctx.Acceleration, 0),
            _ => throw new ArgumentOutOfRangeException()
        };

        ctx.Body.Velocity += movement + SinusNoise(ctx, (float) deltaTime);
        if (ctx.Body.Velocity.Length() >= ctx.MaxSpeed)
            ctx.Body.Velocity = ctx.Body.Velocity.Normalized() * ctx.MaxSpeed;
        
        ctx.Body.MoveAndSlide();
    }

    private Vector2 SinusNoise(WormStateManager ctx, float deltaTime)
    {
        _time += deltaTime;
        var v =
            new Vector2(0, ctx.MovementAmplitude * Mathf.Sin(2 * Mathf.Pi * ctx.MovementDeviation * _time))
                .Normalized() * 50;
        return v;
    }
    
    private void CreateGotoPositions(WormStateManager ctx)
    {
        var leftPos = new Area2D();
        GotoPosSetup(leftPos, WormDirection.Right);
        Debug(ctx, leftPos);
        var rightPos = new Area2D();
        GotoPosSetup(rightPos, WormDirection.Left);
        Debug(ctx, rightPos);
        return;

        void GotoPosSetup(Area2D pos, WormDirection dir)
        {
            pos.GlobalPosition = dir == WormDirection.Left ? _rightPosition : _leftPosition;
            var cs = new CollisionShape2D();
            cs.GlobalPosition = pos.GlobalPosition;
            var shape = new RectangleShape2D();
            shape.Size = shape.Size with { Y = ctx.TurnRange };
            cs.Shape = shape;
            pos.AddChild(cs);
            pos.AreaEntered += other =>
            {
                if (!other.IsInGroup("WormGotoTrigger")) return;
                _direction = dir == WormDirection.Left ? WormDirection.Right : WormDirection.Left;
            };
            ctx.AddChild(pos);
        }
        
    }

    private static void Debug(WormStateManager ctx, Area2D pos)
    {
        if (!ctx.Debug) return;
        if (ctx.DebugSprite == null) throw new MissingMemberException("Debug Sprite is null");
        var sprite = new Sprite2D();
        sprite.Texture = ctx.DebugSprite;
        sprite.RotationDegrees = 90;
        sprite.Scale = sprite.Scale with { X = ctx.TurnRange };
        sprite.GlobalPosition = pos.GlobalPosition;
        pos.AddChild(sprite);
    }
}