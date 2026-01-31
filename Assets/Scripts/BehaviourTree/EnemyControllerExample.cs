using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BehaviourTree.Core;
using BehaviourTree.UnityCore;
using UnityEngine;
using UnityEngine.AI;
using static BehaviourTree.Leaves.BossTasks;

namespace Entities.Enemy {
  public class EnemyControllerExample : AIController {
    public const string CircleInOut = "CircleInOut";
    public const string RoseRangeCooldownKey = "RoseRange";
    public const string CircleRangeCooldownKey = "CircleRange";
    public const string SpecialCooldownKey = "Special";

    [Header("Boss Settings")]
    [SerializeField] private string _name;
    [SerializeField] private Transform _room;
    [SerializeField] private float _secondPhaseEnableThreshold = 0.8f;
    [SerializeField] private float _secondPhaseGuaranteedThreshold = 0.6f;

    [Header("Projectile and Ranged Attacks Settings")]
    [SerializeField] private int _projectilePoolSize;
    [SerializeField] private float _roseAttackDuration;
    [SerializeField] private float _roseAttackSpawnCooldown;
    [SerializeField] private float _projectileSpeed;
    [SerializeField, Tooltip("Number of circle fired when in first phase")] private int _circleAttackRepMin = 2;
    [SerializeField, Tooltip("Number of circle fired when in second phase")] private int _circleAttackRepMax = 3;
    [SerializeField, Tooltip("Time between each circle (in s)")] private float _circleAttackIterCooldown;
    [SerializeField] private GameObject _projectile;
    [SerializeField] private float _circleInOutPauseDuration = 1.5f;
    [SerializeField] private float _circleInOutRange = 5f;
    [SerializeField] private float _circleInOutProjectileSpeedCoef = 2f;

    [Header("Special Attack Settings")]
    [SerializeField] private GameObject _laserBeamPrefab;
    [SerializeField, Tooltip("Duration of the laser growth (in s)")] private float _laserGrowthDuration;
    [SerializeField, Tooltip("Angular velocity to reach during the windup")] private float _laserBeamAngularVelocity;
    [SerializeField, Tooltip("Duration of the laser beam windup (in s)")] private float _laserBeamWindUpDuration;
    [SerializeField, Tooltip("Duration of the laser beam rotation (in s)")] private float _laserBeamDuration;

    [Header("Technical Settings")]
    [SerializeField, Tooltip("Anguler velocity for orbs rotation when idle")] private float _idleAngularVelocity;
    [SerializeField, Tooltip("The coefficient applied to the idle angular velocity when moving")] private float _movingAngularVelocityCoef;
    [SerializeField, Tooltip("The coefficient applied to the idle angular velocity when doing a ranged attack")] private float _rangedAttackAngularVelocityCoef;
    [SerializeField, Tooltip("Duration for ranged attacks windup (in s)")] private float _rangedAttackAccelerationDuration;
    [SerializeField, Tooltip("Duration for both fade in and fade out for teleport animation (in s)")] private float _fadingDuration;

    [Header("Orbs Settings")]
    [SerializeField] private GameObject _orbPrefab;
    [SerializeField] private GameObject _orbTargetPrefab;
    [SerializeField] private float _orbQuantity;
    [SerializeField] private float _orbitDistance;

    [Header("Cooldowns")]
    [SerializeField, Tooltip("Amount of time the armor skill can be used")] private int _amoutOfArmor;
    [SerializeField] private float _rangedAttackCooldown;
    [SerializeField] private float _specialAttackCooldown;
    [SerializeField] private float _circleInOutCooldown;
    [SerializeField] private float _armorCooldown;

    [Header("Children components")]
    [SerializeField] private Transform _gfx;
    [SerializeField] private Transform _shadow;

    [Header("Death settings")]
    [SerializeField] private float _deathDuration;

    private Vector3 _previousPosition;
    private float _realOrbitDistance, _currentHealth;
    private int _armorUsed = 0;
    private bool _isInSecondPhase = false;
    private NavMeshAgent _navAgent;

    private static readonly int[] _orbsPhaseOne = new int[] { 0, 3 };
    private static readonly int[] _orbsPhaseTwo = new int[] { 0, 2, 4 };

    public bool IsInSecondPhase => _isInSecondPhase;
    public float SecondPhaseEnableThreshold => _secondPhaseEnableThreshold;
    public float SecondPhaseGuaranteedThreshold => _secondPhaseGuaranteedThreshold;
    public float SecondPhaseThresholdRange => _secondPhaseEnableThreshold - _secondPhaseGuaranteedThreshold;
    private int CircleAttackRep => _isInSecondPhase ? _circleAttackRepMax : _circleAttackRepMin;
    public float CurrentHealth => _currentHealth;
    public bool CanArmor => 0 < _amoutOfArmor - _armorUsed;
    public string Name => _name;

    #region Unity Callbacks
    protected override void Awake() {
      base.Awake();

      _navAgent = GetComponent<NavMeshAgent>();

      _navAgent.enabled = false;


      for (int i = 0; i < 2; i++) {
      }

      DisableTree();

    }

    protected override void FixedUpdate() {
      base.FixedUpdate();

      // Updating orbs' rotating direction based on last position
      if (transform.position == _previousPosition) {
        return;
      }

      float estimatedVelocity = ((transform.position - _previousPosition) / Time.fixedDeltaTime).x;
    }

    public override void OnDeath() {
      DisableTree();
      DisableCollision();
      StopAllCoroutines();
    }
    #endregion

    #region Objects Generation
    protected override Node ConstructBehaviorTree() {
      // Creating state setter leaf nodes
      CircleInOut circleInOutAttack = new(this);
      PerformRoseAttack roseRangedAttack = new(this);
      PerformSpecialAttack specialAttack = new(this);
      PerformCircleRangedAttack circleRangedAttack = new(this);
      Move move = new(this);
      Idle idle = new(this);
      SwitchPhase switchPhase = new(this);
      Armor armor = new(this);

      Repeater circleInOutSequence = new(circleInOutAttack, 2);
      List<Node> sequences = new() { idle, move, roseRangedAttack, specialAttack, switchPhase, circleRangedAttack, circleInOutSequence/*, armor*/};

      // Construct the behavior tree
      return new Repeater(new AdvancedSelector(sequences));
    }
    #endregion

    #region Helpers
    public void RoseRangedAttack(int iter = 0) {
      int spawnerForRoseAttack = _isInSecondPhase ? 3 : 2;
      int orbIndex = iter * (spawnerForRoseAttack == 2 ? 3 : 2);


      if (iter < spawnerForRoseAttack - 1) {
        RoseRangedAttack(++iter);
      }
    }

    public void CircleAttack() {
    }

    private void ShootFromPool(Vector3 position, Vector3 direction) {
    }

    private void CircleInOutAttack() {


    }

    private void SendProjectileForth(Vector3 origin) {
    }

    private void CallBackProjectiles() {
    }

    protected override void Reset() {
      base.Reset();

      DisableTree();
      _navAgent.enabled = false;
      //_bossInfos.Hide(); --> TODO: fix coroutine issue
    }

    public void MoveDownAnimation(float duration) {
      Reset();

      float scale = _shadow.localScale.x; // should be 0.9f
      Vector3 pos = transform.position;

      _shadow.localScale = new(0, _shadow.localScale.y, 1);
      transform.position = _room.position;
      _gfx.position = pos;

      DisableCollision();
    }

    private void Ready() {
      EnableCollision();
      EnableTree();
      _navAgent.enabled = true;
    }
    #endregion

    #region State Change Handling
    public Coroutine PerfomSpecialAttack(Action onComplete) => StartCoroutine(SpecialAttackCoroutine(onComplete));
    public Coroutine PerformCircleRangedAttack(Action onComplete) => StartCoroutine(CircleAttackCoroutine(onComplete));
    public Coroutine SwitchPhase(Action onComplete) => StartCoroutine(SwitchPhaseCoroutine(onComplete));
    public Coroutine SetRoseAttackState(Action onComplete) => StartCoroutine(RoseAttackCoroutine(onComplete));
    public Coroutine PerformCircleInOutAttack(Action onComplete) => StartCoroutine(CircleInOutCoroutine(onComplete));
    public Coroutine Armoring(Action onComplete) {
      //todo
      _armorUsed++;
      return null;
    }
    #endregion

    #region States Coroutines
    protected override IEnumerator IdleCoroutine(Action onComplete) {

      // Idle time:
      // Fast-Paced: 0.2 to 0.5
      // Moderate-Paced: 0.5 to 1.5
      // Slow-Paced: 1.5 to 3
      yield return new WaitForSeconds(UnityEngine.Random.Range(0.5f, 1.5f));
      onComplete?.Invoke();
    }

    private IEnumerator RoseAttackCoroutine(Action onComplete) {
      int[] selectedOrbs = _isInSecondPhase ? _orbsPhaseTwo : _orbsPhaseOne;


      float elapsedTime = 0;

      while (elapsedTime < _roseAttackDuration) {
        elapsedTime += _roseAttackSpawnCooldown;
        RoseRangedAttack();
        yield return new WaitForSeconds(_roseAttackSpawnCooldown);
      }

      SetCooldown(RoseRangeCooldownKey, _rangedAttackCooldown);
      onComplete?.Invoke();
    }

    private IEnumerator SpecialAttackCoroutine(Action onComplete) {
      if (0.1f < Vector3.Distance(transform.position, _room.position)) {
      }





      yield return new WaitForSeconds(_laserBeamWindUpDuration);
      yield return new WaitForSeconds(_laserBeamDuration);



      SetCooldown(SpecialCooldownKey, _specialAttackCooldown);
      onComplete?.Invoke();
    }

    private IEnumerator SwitchPhaseCoroutine(Action onComplete) {
      _isInSecondPhase = true;
      yield return null;

      // todo: changing overall colors?
      // todo: audio cue?
      onComplete?.Invoke();
    }

    static readonly private int[] _allOrbs = new int[] { 0, 1, 2, 3, 4, 5 };

    private IEnumerator CircleAttackCoroutine(Action onComplete) {
      int iter = 0;


      while (iter < CircleAttackRep) {
        iter++;
        CircleAttack();
        yield return new WaitForSeconds(_circleAttackIterCooldown);
      }

      SetCooldown(CircleRangeCooldownKey, _rangedAttackCooldown);
      onComplete?.Invoke();
    }

    private IEnumerator CircleInOutCoroutine(Action onComplete) {

      CircleInOutAttack();
      yield return new WaitForSeconds(_circleInOutPauseDuration);
      CallBackProjectiles();

      SetCooldown(CircleInOut, _circleInOutCooldown);
      onComplete?.Invoke();
    }
    #endregion
  }
}
