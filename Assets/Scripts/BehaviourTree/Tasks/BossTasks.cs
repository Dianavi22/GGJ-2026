using System;
using BehaviourTree.Core;
using UnityEngine;
using Entities.Enemy;

namespace BehaviourTree.Leaves {
  public class BossTasks {
    public class PerformRoseAttack : CoroutineNode {
      private readonly EnemyControllerExample _boss;

      public PerformRoseAttack(EnemyControllerExample boss) {
        _boss = boss;
      }

      protected override Coroutine StartAction(Action onComplete) => _boss.SetRoseAttackState(onComplete);

      //		public override float GetModifiedWeight() {
      //			float weight = GetBaseWeight();

      //			if (_boss.IsOnCooldown(BossAnimator.RoseRangeCooldownKey)) {
      //				return 0;
      //			}

      //			if (!_boss.IsPlayerInMeleeRange) {
      //				weight += 0.5f;
      //			}

      //			return weight;
      //		}
    }

    public class PerformCircleRangedAttack : CoroutineNode {
      private readonly EnemyControllerExample _boss;

      public PerformCircleRangedAttack(EnemyControllerExample boss) {
        _boss = boss;
      }

      protected override Coroutine StartAction(Action onComplete) => _boss.PerformCircleRangedAttack(onComplete);

      //		public override float GetModifiedWeight() {
      //			float weight = GetBaseWeight();

      //			if (_boss.IsOnCooldown(BossAnimator.CircleRangeCooldownKey)) {
      //				return 0;
      //			}

      //			if (!_boss.IsPlayerInMeleeRange) {
      //				weight += 0.5f;
      //			}

      //			return weight;
      //		}
    }

    public class PerformSpecialAttack : CoroutineNode {
      private readonly EnemyControllerExample _boss;

      public PerformSpecialAttack(EnemyControllerExample boss) {
        _boss = boss;
      }

      protected override Coroutine StartAction(Action onComplete) => _boss.PerfomSpecialAttack(onComplete);

      //		public override float GetModifiedWeight() {
      //			float weight = GetBaseWeight();

      //			if (_boss.IsOnCooldown(BossAnimator.SpecialCooldownKey) || !_boss.IsInSecondPhase) {
      //				return 0;
      //			}

      //			weight += 1; // todo: scale with health after secondphase?

      //			return weight;
      //		}
    }

    public class Idle : CoroutineNode {
      private readonly EnemyControllerExample _boss;

      public Idle(EnemyControllerExample boss) {
        _boss = boss;
      }

      protected override Coroutine StartAction(Action onComplete) => _boss.SetIdleState(onComplete);

      //		public override float GetBaseWeight() {
      //			return 0.2f;
      //		}

      //		public override float GetModifiedWeight() {
      //			float weight = GetBaseWeight();

      //			if (_boss.AllOnCooldown) {
      //				weight += 100f; // Guranteed if all is on cooldown
      //			}

      //			return weight;
      //		}
    }

    public class Move : CoroutineNode {
      private readonly EnemyControllerExample _boss;

      public Move(EnemyControllerExample boss) {
        _boss = boss;
      }

      protected override Coroutine StartAction(Action onComplete) => _boss.SetMovingState(onComplete);

      //		public override float GetBaseWeight() {
      //			return 0.4f;
      //		}

      //		public override float GetModifiedWeight() {
      //			float weight = GetBaseWeight();

      //			return weight;
      //		}
    }

    public class SwitchPhase : CoroutineNode {
      private readonly EnemyControllerExample _boss;

      public SwitchPhase(EnemyControllerExample boss) {
        _boss = boss;
      }

      protected override Coroutine StartAction(Action onComplete) => _boss.SwitchPhase(onComplete);

      //		public override float GetModifiedWeight() {
      //			// Can't use it again if already in second phase
      //			if (_boss.IsInSecondPhase) {
      //				return 0;
      //			}

      //			float healthPercentage = _boss.HittableEntity.CurrentHealthPercentage;

      //			if (_boss.SecondPhaseEnableThreshold < healthPercentage) { // Can't go in second phase if above 60%
      //				return 0;
      //			} else if (healthPercentage < _boss.SecondPhaseGuaranteedThreshold) {
      //				return 100; // Guaranteed if below 40% health
      //			}

      //			float weight = GetBaseWeight();

      //			// Normalizing the range [0.4; 0.6] to [0; 1] for easing.
      //			weight += Easing.InQuad((healthPercentage - _boss.SecondPhaseGuaranteedThreshold) / _boss.SecondPhaseThresholdRange);

      //			return weight;
      //		}
    }

    public class CircleInOut : CoroutineNode {
      private readonly EnemyControllerExample _boss;

      public CircleInOut(EnemyControllerExample boss) {
        _boss = boss;
      }

      protected override Coroutine StartAction(Action onComplete) => _boss.PerformCircleInOutAttack(onComplete);

      //		public override float GetModifiedWeight() {
      //			float weight = GetBaseWeight();
      //			
      //			if (_boss.IsOnCooldown(BossAnimator.CircleInOut)) {
      //				return 0;
      //			}

      //			if (_boss.IsPlayerInMeleeRange) {
      //				weight += 0.5f;
      //			}

      //			return weight;
      //		}
    }

    public class Armor : CoroutineNode {
      private readonly EnemyControllerExample _boss;

      public Armor(EnemyControllerExample boss) {
        _boss = boss;
      }

      protected override Coroutine StartAction(Action onComplete) => _boss.Armoring(onComplete);

      //		public override float GetModifiedWeight() {
      //			if (!_boss.IsInSecondPhase || !_boss.CanArmor) {
      //				return 0;
      //			}

      //			float weight = GetBaseWeight();

      //			weight += 1 - _boss.HittableEntity.CurrentHealthPercentage;

      //			return weight;
    }
    }
  }

