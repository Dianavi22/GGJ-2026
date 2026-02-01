using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using BehaviourTree.Core;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

namespace BehaviourTree.UnityCore
{
    [RequireComponent(typeof(NavMeshAgent))]
    public abstract class AIController : MonoBehaviour
    {
        [Header("AI Controller")]
        [SerializeField, Tooltip("Range of the aggro state (in m, used as Stopping Distance as well)")] private float _aggroRange;

        [Header("Debug")]
        [SerializeField, Tooltip("Overrides the BT evaluation according to Evaluate Tree Override")] private bool _shouldOverrideTreeEvaluation = false;
        [SerializeField, Tooltip("Overrides the BT evaluation to Overrde Tree Evaulation")] private bool _evaluateTreeOverride = false;
        [SerializeField, Tooltip("Show the melee range with the Gizmos")] private bool _showMeleeRangeGizmos = false;

        private bool _evaluateTree = false, _canMove = false;
        private Node _root;
        protected NavMeshAgent _agent;
        [SerializeField] protected Transform _player;

        protected Transform _target;
        [SerializeField] protected Transform _spawn;
        private readonly Dictionary<string, float> _cooldowns = new();
        private List<Collider> _colliders;

        #region Getters
        public bool IsPlayerInAggroRange => _player != null && Vector3.Distance(transform.position, _player.transform.position) < _aggroRange;
        public bool AllOnCooldown => _cooldowns.All((e) => 0 < e.Value) && _cooldowns.Count != 0;
        #endregion

        #region Unity Callbacks
        protected virtual void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            _colliders = GetComponentsInChildren<Collider>().ToList();

            _root = ConstructBehaviorTree();
            _evaluateTree = true;

            _agent.updateRotation = false;
            _agent.updateUpAxis = false;
            _agent.stoppingDistance = _aggroRange;
        }

        protected virtual void Start()
        {
            Reset();
        }

        protected virtual void Update()
        {
            if ((!_shouldOverrideTreeEvaluation && _evaluateTree) || (_shouldOverrideTreeEvaluation && _evaluateTreeOverride))
            {
                _root.Evaluate();
            }
        }

        protected virtual void FixedUpdate()
        {
            if (_canMove && _target != null)
            {
                _agent.destination = _target.position; // Moving toward the player
            }

            // Decrementing cooldowns
            for (int i = 0; i < _cooldowns.Count; i++)
            {
                KeyValuePair<string, float> a = _cooldowns.ElementAt(i);
                _cooldowns[a.Key] = a.Value - Time.deltaTime;
            }
        }

        protected virtual void OnTriggerEnter(Collider other)
        {

            // Here TODO if other trygetcomponent visionCone
            // if (other.TryGetComponent(out PlayerController player))
            // {
            //     _player = player.transform;
            // }

        }

        private void OnDrawGizmos()
        {
            if (!_showMeleeRangeGizmos)
            {
                return;
            }

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _aggroRange);
        }
        #endregion

        #region Abstract & Virtual methods
        protected abstract Node ConstructBehaviorTree();
        protected abstract IEnumerator IdleCoroutine(Action onComplete);

        protected virtual void OnMove() { }

        protected virtual void Reset()
        {
            transform.position = _spawn.position;
            StopAllCoroutines();

            for (int i = 0; i < _cooldowns.Count; i++)
            {
                _cooldowns.Values.ToList()[i] = 0;
            }
        }
        #endregion

        protected void SetCooldown(string key, float cooldown) => _cooldowns[key] = cooldown;
        protected void DisableTree() => _evaluateTree = false;
        protected void EnableTree() => _evaluateTree = true;

        public Coroutine SetIdleState(Action onComplete) => StartCoroutine(IdleCoroutine(onComplete));
        public Coroutine SetMovingState(Action onComplete) => StartCoroutine(MoveCoroutine(onComplete));

        protected IEnumerator MoveCoroutine(Action onComplete)
        {
            _canMove = true;
            OnMove();
            yield return new WaitForSeconds(UnityEngine.Random.Range(0.1f, .5f));
            onComplete?.Invoke();
            _canMove = false;
        }


        public bool IsOnCooldown(string key)
        {
            if (_cooldowns.TryGetValue(key, out float cd))
            {
                return 0 < cd;
            }

            return false;
        }

        protected void DisableCollision()
        {
            _colliders.ForEach(collider => collider.enabled = false);
        }

        protected void EnableCollision()
        {
            _colliders.ForEach(collider => collider.enabled = true);
        }
    }
}

