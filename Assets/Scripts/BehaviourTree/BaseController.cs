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
    public class BaseController : AIController
    {
        public bool AttackMode = false;
        public bool IsSeen = false;

        [SerializeField] private Transform _spawnPoint;
        protected NavMeshAgent _navAgent;
        protected FieldOfView _fov;
        protected PlayerController _playerController;
        protected override void Awake()
        {
            base.Awake();
            _navAgent = GetComponent<NavMeshAgent>();
            _fov = _player.GetComponent<FieldOfView>();
            _playerController = _player.GetComponent<PlayerController>();

            _navAgent.acceleration = 0.3f;
            EnableTree();
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
        }

        #region Objects Generation
        protected override Node ConstructBehaviorTree()
        {
            Move move = new(this);
            Idle idle = new(this);
            Attack attack = new(this);
            IdleAttack idleAttack = new(this);
            SwitchTarget switchTargetCoroutine = new(this);

            List<Node> sequences = new() { idle, move, attack, idleAttack, switchTargetCoroutine };
            return new Repeater(new AdvancedSelector(sequences));
        }
        #endregion
        #region Helpers

        #endregion
        #region State Change Handling

        #endregion
        #region States Coroutines

        protected override IEnumerator IdleCoroutine(Action onComplete)
        {
            yield return new WaitForSeconds(UnityEngine.Random.Range(0.2f, 0.5f));
            onComplete?.Invoke();
        }

        protected override IEnumerator AttackCoroutine(Action onComplete)
        {
            _target = _player;
            AttackMode = true;
            _navAgent.acceleration = 1f;
            yield return null;
        }

        protected override IEnumerator IdleAttackCoroutine(Action onComplete)
        {
            // Do nothing, to implement in children
            yield return null;
        }


        protected override IEnumerator SwitchTargetState(Action onComplete)
        {
            // Switch between player and spawnPoint as targets when moving
            // Switch should nly occur when ennemy have been frightened by player (with mask)
            // Or when ennemy get back to hunting player
            if (_target != _player)
            {
                _target = _player;
            }
            else if (_target != _spawnPoint)
            {
                _target = _spawnPoint;
            }
            Debug.Log("hop hop on change");
            yield return null;
            onComplete?.Invoke();
        }

        #endregion
    }
}