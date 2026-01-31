using System;
using System.Collections;
using System.Collections.Generic;
using BehaviourTree.Core;
using BehaviourTree.UnityCore;
using UnityEngine;
using UnityEngine.AI;
using static BehaviourTree.Leaves.BaseTasks;

namespace Entities.Enemy
{
    public class BaseController : AIController
    {

        private NavMeshAgent _navAgent;

        protected override void Awake()
        {
            base.Awake();

            _navAgent = GetComponent<NavMeshAgent>();

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

            List<Node> sequences = new() { idle, move };
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

        #endregion
    }
}