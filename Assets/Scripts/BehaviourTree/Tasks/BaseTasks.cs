using System;
using BehaviourTree.Core;
using UnityEngine;
using Entities.Enemy;

namespace BehaviourTree.Leaves
{
  public class BaseTasks
  {
    public class Idle : CoroutineNode
    {
      private readonly BaseController _character;

      public Idle(BaseController character)
      {
        _character = character;
      }

      protected override Coroutine StartAction(Action onComplete) => _character.SetIdleState(onComplete);

      public override float GetBaseWeight()
      {
        return 0.3f;
      }

      public override float GetModifiedWeight()
      {
        float weight = GetBaseWeight();

        if (_character.AllOnCooldown)
        {
          weight += 100f; // Guranteed if all is on cooldown
        }

        return weight;
      }
    }

    public class Move : CoroutineNode
    {
      private readonly BaseController _character;

      public Move(BaseController character)
      {
        _character = character;
      }

      protected override Coroutine StartAction(Action onComplete) => _character.SetMovingState(onComplete);

      public override float GetBaseWeight()
      {
        return 0.8f;
      }

      public override float GetModifiedWeight()
      {

        float weight = GetBaseWeight();

        return weight;
      }
    }

    public class Flee : CoroutineNode{
      private readonly BaseController _character;

      public Flee(BaseController character) {
        _character = character;
      }

      protected override Coroutine StartAction(Action onComplete) => _character.SetFleeingState(onComplete);

      public override float GetBaseWeight() => 0;

      public override float GetModifiedWeight()
      {
        return _character.Status == BaseController.MaskState.feared ? 100 : 0;
      }
    }

    public class Attack : CoroutineNode{
      private readonly BaseController _character;

      public Attack(BaseController character) {
        _character = character;
      }

      protected override Coroutine StartAction(Action onComplete) => _character.SetAttackState(onComplete);

      public override float GetBaseWeight() => 0;

      public override float GetModifiedWeight()
      {
        return _character.Status == BaseController.MaskState.aggroed ? 100 : 0;
      }
    }
 
  }
}
