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
      ChangeState(MaskState.roaming);
    }

    protected override void Update() {
      if(_state == MaskState.aggroed) {
        //TODO: custom behaviour
      } else {
        base.Update();
      }

      IsSeen = _playerController.ActiveMask == _weakness && _fov != null && _fov.visibleTargets.Count > 0 && _fov.visibleTargets.Find((target) => target == transform) && !IsSeen;

      //TODO: TO REMOVE DEBUG!!!!
      if(Input.GetKeyDown(KeyCode.F)) {
        ChangeState(MaskState.feared);
      }

      if(Input.GetKeyDown(KeyCode.C)){
        ChangeState(MaskState.roaming);
      }
    }

    protected override void FixedUpdate()
    {
      base.FixedUpdate();

    }

    private void ChangeState(MaskState newState){
      _state = newState;
      SwitchTarget();
    }

    private void SwitchTarget()
    {
      // Switch between player and spawnPoint as targets when moving
      // Switch should only occur when ennemy have been frightened by player (with mask)
      // Or when ennemy get back to hunting player
      _target = _state switch {
        MaskState.feared => _spawn,
        _ => _player
      };

      _agent.speed = _state switch {
        MaskState.feared => _fearedSpeed,
        _ => _baseSpeed
      };
    }

    #region Objects Generation
    protected override Node ConstructBehaviorTree()
    {
      //TODO: each child object has to construct its tree based on this one which has idle and random movement.
      Move move = new(this);
      Idle idle = new(this);

      List<Node> sequences = new() { idle, move /*, ...AggroedBehaviour()*/};
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
    #endregion
  }
}
