using System;
using System.Collections;
using System.Collections.Generic;
using BehaviourTree.Core;
using BehaviourTree.UnityCore;
using UnityEngine;
using UnityEngine.AI;
using static BehaviourTree.Leaves.BaseTasks;
using UnityEngine.SceneManagement;


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

    private MaaskExpressons _mask;

    protected FieldOfView _fov;
    protected PlayerController _playerController;
        public String ending;

    protected MaskState _state;
    public MaskState Status => _state;

        private float _elapsed;

    protected override void Awake()
    {
      base.Awake();

      _mask = GetComponentInChildren<MaaskExpressons>();
      _agent.speed = _baseSpeed;
    }



        protected override void Start(){
      base.Start(); 
      _fov = _player.GetComponentInChildren<FieldOfView>();
      _playerController = _player.GetComponent<PlayerController>();
      _state = MaskState.roaming;
      _target = _player;
    }

    protected override void Update() {
      if(_state == MaskState.feared) return;
      base.Update();

      _state = IsPlayerInAggroRange ? MaskState.aggroed : MaskState.roaming;

      _mask.isAgro = _state == MaskState.aggroed;

            if (_state == MaskState.aggroed) {
                _elapsed += Time.deltaTime ;
            } else
            {
                _elapsed = 0;
            }

            if (_elapsed > 2f) {
                SceneManager.LoadScene(ending);
            }

      IsSeen = _playerController.ActiveMask == _weakness && _fov != null && _fov.visibleTargets.Count > 0 && _fov.visibleTargets.Find((target) => target == transform);

        if (IsSeen || Input.GetKeyDown(KeyCode.L)) {
                //StopAllCoroutines()
            StartCoroutine(FleeCoroutine(() => base.ResetRoot()));
        _mask.isRotating = true;
        _state = MaskState.feared;
      }
    }

    #region Objects Generation
    protected override Node ConstructBehaviorTree()
    {
      Move move = new(this);
      Flee flee = new(this);
      Idle idle = new(this);

            List<Node> sequences = new() { idle, move, flee };
      return new Repeater(new AdvancedSelector(sequences));
    }
        #endregion

        #region States Coroutines
        protected override abstract IEnumerator AttackCoroutine(Action onComplete);



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
      _mask.isRotating = false;
      _state = MaskState.roaming;
    }
    #endregion
  }
}
