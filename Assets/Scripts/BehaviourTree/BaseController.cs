using System;
using System.Collections;
using System.Collections.Generic;
using BehaviourTree.Core;
using BehaviourTree.UnityCore;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.AI;
using static BehaviourTree.Leaves.BaseTasks;

namespace Entities.Enemy
{
  public abstract class BaseController : AIController
  {
    public enum MaskState { roaming, aggroed, feared };

    public bool AttackMode = false;
    public bool IsSeen = false;

    [SerializeField] private PlayerController.Masks _weakness;
    [SerializeField] private float _baseSpeed;
    [SerializeField] private float _fearedSpeed;

    protected FieldOfView _fov;
    protected PlayerController _playerController;

    protected MaskState _state;
    public MaskState Status => _state;

    protected override void Awake()
    {
      base.Awake();
    }

    protected override void Start(){
      base.Start(); 
      _fov = _player.GetComponent<FieldOfView>();
      _playerController = _player.GetComponent<PlayerController>();
      _state = MaskState.roaming;
      _target = _player;
    }

    protected override void Update() {
      base.Update();

      if(_state == MaskState.feared) return;

      if(IsPlayerInAggroRange && _state == MaskState.roaming) {
        _state = MaskState.aggroed;
      }

      IsSeen = _playerController.ActiveMask == _weakness && _fov != null && _fov.visibleTargets.Count > 0 && _fov.visibleTargets.Find((target) => target == transform);

      if(IsSeen || Input.GetKeyDown(KeyCode.L)) {
//        StopAllCoroutines();
        _state = MaskState.feared;
      }
    }

    #region Objects Generation
    protected override Node ConstructBehaviorTree()
    {
      //TODO: each child object has to construct its tree based on this one which has idle and random movement.
      Move move = new(this);
      Flee flee = new(this);
      Idle idle = new(this);

      List<Node> sequences = new() { idle, move, flee /*, ...AggroedBehaviour()*/};
      return new Repeater(new AdvancedSelector(sequences));
    }

    //TODO: uncomment next line
    // protected abstract List<Node> AggroedBehaviour();
    #endregion

    #region States Coroutines
    // Idling for random amount of time
    protected override IEnumerator IdleCoroutine(Action onComplete)
    {
      yield return new WaitForSeconds(UnityEngine.Random.Range(0.2f, 0.5f));
      onComplete?.Invoke();
    }

    // Fleeing for a random amount of time
    protected override IEnumerator FleeCoroutine(Action onComplete)
    {
      _canMove = true;
      _target = _spawn;
      _agent.speed = _fearedSpeed;
      yield return new WaitForSeconds(UnityEngine.Random.Range(1.0f, 2.5f));
      onComplete?.Invoke();
      _canMove = false;
      _target = _player;
      _agent.speed = _baseSpeed;
      _state = MaskState.roaming;
    }
    #endregion
  }
}
