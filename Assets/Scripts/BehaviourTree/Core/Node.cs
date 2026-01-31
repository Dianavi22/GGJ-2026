using System;
using UnityEngine;

namespace BehaviourTree.Core {
    public enum NodeState { Running, Success, Failure }

    public abstract class Node {
        protected NodeState state = NodeState.Running;

        public abstract NodeState Evaluate();

        public virtual void Reset() {
            state = NodeState.Running;
        }

        public virtual float GetBaseWeight() => 1.0f;
        public virtual float GetModifiedWeight() => GetBaseWeight();
    }

    /// <summary>
    /// Used for nodes which relies on a coroutine to complete. Used for Coroutine animation for example.
    /// This node runs the coroutine only once.
    /// </summary>
    public abstract class CoroutineNode : Node {
        private bool _isComplete = false;
        private Coroutine _coroutine;

        public override NodeState Evaluate() {
            if (!_isComplete) {
                _coroutine ??= StartAction(OnActionComplete);
                state = NodeState.Running;
                return state;
            }

            state = NodeState.Success;
            return state;
        }

        protected abstract Coroutine StartAction(Action onComplete);

        public override void Reset() {
            base.Reset();

            _isComplete = false;
            _coroutine = null;
        }

        private void OnActionComplete() {
            _isComplete = true;
        }
    }
}


