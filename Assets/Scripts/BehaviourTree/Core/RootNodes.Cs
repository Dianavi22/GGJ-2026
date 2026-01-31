using System;

namespace BehaviourTree.Core {
    /// <summary>
    /// Invert the value of its child node.
    /// Useful when creating complex conditions by using the opposite of already existings conditions.
    /// <example>For example: player not in melee range = Inverter(IsPlayerInMeleeRange())</example>
    /// </summary>
    public class Inverter : Node {
        private Node _node;

        public Inverter(Node node) {
            _node = node;
        }

        public override NodeState Evaluate() {
            state = _node.Evaluate() switch {
                NodeState.Success => NodeState.Failure,
                NodeState.Failure => NodeState.Success,
                NodeState.Running => NodeState.Running,
                _ => throw new Exception("Enum type exception occured in Inverter::Evaluate")
            };

            return state;
        }

        public override float GetModifiedWeight() {
            return _node.GetModifiedWeight();
        }
    }

    /// <summary>
    /// Repeat its child node a number of time; indefinitely if given a negative value.
    /// </summary>
    public class Repeater : Node {
        private readonly Node _node;
        private readonly int _repeatCount;
        private int _currentCount;

        public Repeater(Node node, int repeat) {
            _node = node;
            _repeatCount = repeat;
            _currentCount = 0;
        }

        public Repeater(Node node) : this(node, -1) { }

        public override void Reset() {
            _currentCount = 0;
            _node.Reset();
            base.Reset();
        }

        public override NodeState Evaluate() {
            // If all iterations have been completed
            if (0 < _repeatCount && _repeatCount <= _currentCount) {
                state = NodeState.Success;
                return state;
            }

            NodeState childState = _node.Evaluate();

            // Evaluates to running if the child is running
            if (childState == NodeState.Running) {
                state = NodeState.Running;
                return state;
            }

            // If the child is done, we reset it and we increase the count iterator if it's not an infinite repeater
            if (childState == NodeState.Success || childState == NodeState.Failure) {
                _node.Reset();

                if (0 < _repeatCount) {
                    _currentCount++;

                    // If we repeated enough, we return success
                    if(_repeatCount <= _currentCount) {
                        state = NodeState.Success;
                        return state;
                    }
                }
            }

            state = NodeState.Running;
            return state;
        }

        public override float GetModifiedWeight() {
            return _node.GetModifiedWeight();
        }
    }
}


