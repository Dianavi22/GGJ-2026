using System.Collections.Generic;
using System.Linq;

namespace BehaviourTree.Core {
	// A selector exectues its children from left to right until one succeeds.
	// The selector succeeds if one succeeds.
	// If all fail, the selector fails.
	public class Selector : Node {
		private readonly List<Node> _nodes;
		private int _currentIndex = 0;

		public Selector(List<Node> nodes) {
			_nodes = nodes;
		}

		public override void Reset() {
			base.Reset();
			_currentIndex = 0;
			_nodes.ForEach((node) => node.Reset());
		}

		public override NodeState Evaluate() {
			if (_nodes.Count <= _currentIndex) {
				state = NodeState.Failure;
				return state;
			}

			NodeState currentNodeState = _nodes[_currentIndex].Evaluate();

			if (currentNodeState == NodeState.Running) {
				state = NodeState.Running;
				return state;
			}

			if (currentNodeState == NodeState.Success) {
				state = NodeState.Success;
				return state;
			}

			// if currentNodeState == NodeState.Failure --> moving to next
			_currentIndex++;
			return Evaluate();
		}

		public override float GetModifiedWeight() {
			// Returning the most favourable outcome, as the node succeeds if one child succeeds.
			return _nodes.Select((node) => node.GetModifiedWeight()).Max();
		}
	}

	// A sequence executes its children from left to right until ones fails.
	// The sequence fails if one fails.
	// If all succeed, the sequence succeeds.
	public class Sequence : Node {
		private readonly List<Node> _nodes;
		private int _currentIndex = 0;

		public Sequence(List<Node> nodes) {
			_nodes = nodes;
		}

		public override void Reset() {
			base.Reset();
			_currentIndex = 0;
			_nodes.ForEach((node) => node.Reset());
		}

		public override NodeState Evaluate() {
			if (_nodes.Count <= _currentIndex) {
				state = NodeState.Success;
				return state;
			}

			NodeState currentNodeState = _nodes[_currentIndex].Evaluate();

			if (currentNodeState == NodeState.Running) {
				state = NodeState.Running;
				return state;
			}

			if (currentNodeState == NodeState.Failure) {
				state = NodeState.Failure;
				return state;
			}

			// if currentNodeState == NodeState.Success --> Evaluating the next node
			_currentIndex++;
			return Evaluate();
		}

		public override float GetModifiedWeight() {
			// Returning the lesat favourable outcome, as the node succeeds if all children succeed.
			return _nodes.Select((node) => node.GetModifiedWeight()).Min();
		}
	}

	// An Advanced Selector will choose one child from a list based on their weights.
	// It will evaluate each child node's weight and decide which one to run.
	public class AdvancedSelector : Node {
		private readonly List<Node> _nodes;

		private Node _currentNode = null;

		public AdvancedSelector(List<Node> nodes) {
			_nodes = nodes;
		}

		public override void Reset() {
			base.Reset();
			_nodes.ForEach((node) => node.Reset());
			_currentNode = null;
		}

		public override NodeState Evaluate() {
			// If no node is currently active, pick a new one
			if (_currentNode == null) {
				_currentNode = PickRandomNode();

				if (_currentNode == null) {
					state = NodeState.Failure; // No valid nodes to pick
					return state;
				}
			}

			// Evaluate the current node
			state = _currentNode.Evaluate();
			return state;
		}

		private Node PickRandomNode() {
			List<float> weights = new();
			float totalWeight = 0;

			foreach (Node node in _nodes) {
				float weight = node.GetModifiedWeight();
				weights.Add(weight);
				totalWeight += weight;
			}

			if (totalWeight == 0) {
				return null;
			}

			// We're going to change the order, so we need to keep the corresponding node index
			List<(int idx, float value)> weightedSortedList = new();

			for (int i = 0; i < weights.Count; i++) {
				weightedSortedList.Add((i, weights[i] / totalWeight));
			}

			// Sorting the list based on weight value
			weightedSortedList.Sort((x, y) => x.value.CompareTo(y.value));

			float randomValue = UnityEngine.Random.value;
			float cumulativeWeight = 0;

			for (int i = 0; i < weightedSortedList.Count; i++) {
				cumulativeWeight += weightedSortedList[i].value;
				if (randomValue < cumulativeWeight) {
					return _nodes[weightedSortedList[i].idx];
				}
			}

			return null;
		}

		public override float GetModifiedWeight() {
			//TODO: how to decided what is the weight of the advanced selector.
			return GetBaseWeight();
		}
	}
}
