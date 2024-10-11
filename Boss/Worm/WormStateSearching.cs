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
    [Export] private const int SearchRange = 100;

    [Export] private const int TurnRange = 50;

    private WormDirection _direction;
    public override void EnterState(WormStateManager ctx)
    {
        _startPosition = ctx.Body.GlobalPosition;
        _leftPosition = _startPosition - new Vector2(SearchRange, 0);
        _rightPosition = _startPosition + new Vector2(SearchRange, 0);
        _direction = new[] {WormDirection.Left, WormDirection.Right}[new RandomNumberGenerator().RandiRange(0, 1)];
    }

    public override void UpdateState(WormStateManager ctx)
    {
        var movement = _direction switch
        {
            WormDirection.Left => new Vector2(ctx.Acceleration, 0),
            WormDirection.Right => new Vector2(-ctx.Acceleration, 0),
            _ => throw new ArgumentOutOfRangeException()
        };

        ctx.Body.Velocity += movement;
        ctx.Body.MoveAndSlide();
        
        if (IsNearPoint(ctx.Body.GlobalPosition))
        {
            _direction = _direction == WormDirection.Left ? WormDirection.Right : WormDirection.Left;
        }
    }
    
    private bool IsNearPoint(Vector2 position)
    {
        return _leftPosition.DistanceTo(position) < TurnRange || _rightPosition.DistanceTo(position) < TurnRange;
    }
}