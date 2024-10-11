<<<<<<< HEAD
namespace JordenSindreGJH2024.Boss.Worm;

public abstract class WormState
{
   protected enum StateWorm
   {
      Hunting,
      Searching,
   }

   public abstract void EnterState(WormStateManager ctx);
   public abstract void UpdateState(WormStateManager ctx, double deltaTime);
}

