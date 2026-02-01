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
        return 1f;
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
        return 0.2f;
      }

      public override float GetModifiedWeight()
      {
        if (_character.AttackMode)
        {
          return 100;
        }
        float weight = GetBaseWeight();

        return weight;
      }
    }

    public class Attack : CoroutineNode
    {
      private readonly BaseController _character;

      public Attack(BaseController character)
      {
        _character = character;
      }

      protected override Coroutine StartAction(Action onComplete) => _character.SetAttackState(onComplete);

      public override float GetBaseWeight()
      {
        return 0.2f;
      }

      public override float GetModifiedWeight()
      {
        return 0;
        float weight = GetBaseWeight();

        return weight;
      }
    }

    public class IdleAttack : CoroutineNode
    {
      private readonly BaseController _character;

      public IdleAttack(BaseController character)
      {
        _character = character;
      }

      protected override Coroutine StartAction(Action onComplete) => _character.SetIdleAttackState(onComplete);

      public override float GetBaseWeight()
      {
        return 0.2f;
      }

      public override float GetModifiedWeight()
      {
        return 0;
        float weight = GetBaseWeight();

        return weight;
      }
    }

    public class SwitchTarget : CoroutineNode
    {

      private readonly BaseController _character;

      public SwitchTarget(BaseController character)
      {
        _character = character;
      }
      protected override Coroutine StartAction(Action onComplete) => _character.SetSwitchTargetState(onComplete);

      public override float GetBaseWeight()
      {
        return 0.2f;
      }

      public override float GetModifiedWeight()
      {
        return _character.IsSeen ? 100 : 0;
        float weight = GetBaseWeight();

        return weight;
      }
    }
  }
}

