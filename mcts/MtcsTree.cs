using mcts.backpropagation;
using mcts.selection;
using System;

namespace mcts
{
    sealed public class Tree
    {
        private uint _maxPlayOuts;
        private long _maxTimeOut;
        private ISelectionStrategy _selectionStrategy;
        private IBackPropagationStrategy _backPropagationStrategy;
        private ISelectionStrategy _finalSelectionStrategy;

        /// <summary>
        /// Trả về best Action bắt đầu từ Node này
        /// </summary>
        /// <param name="root">Root node bắt đầu tính</param>
        public IActionProtocol ComputeAction(Node root, Action<string> logger)
        {
            var startPoint = DateTime.Now.Millisecond;
            const uint batchSize = 30;

            for (uint i = 0; i < _maxPlayOuts; i += batchSize)
            {
                for (uint j = 0; j < batchSize; j++)
                {
                    FourStep(root);
                }

                var currentPoint = DateTime.Now.Millisecond;
                var diff = currentPoint - startPoint;
                if (diff > _maxTimeOut)
                {
                    break;
                }
            }

            var bestChild = _finalSelectionStrategy.Select(root);
            return bestChild.Action;
        }

        /// <summary>
        /// Apply 4 steps của Monte Carlo simulation cho node này
        /// </summary>
        private void FourStep(Node root)
        {
            var bestNode = Select(root);
            var expandedNode = Expand(bestNode);
            var finalState = Simulate(expandedNode);
            BackPropagation(expandedNode, finalState);
        }

        /// <summary>
        /// Select ra Best Node
        /// </summary>
        private Node Select(Node root)
        {
            var currentNode = root;

            // Selection.
            // Recursively select the best child of fully expanded nodes.
            // Ignore terminal nodes because they don't have children.
            while (currentNode.IsExpanded && !currentNode.IsTerminal)
            {
                currentNode = _selectionStrategy.Select(currentNode);
            }

            return currentNode;
        }
        private Node Expand(Node bestNode)
        {
            if (bestNode.IsTerminal)
                return bestNode;

            var expandedNode = bestNode.Expand();
            return (Node)expandedNode;
        }

        private IStateProtocol Simulate(Node expandedNode)
        {
            if (expandedNode.IsTerminal)
                return expandedNode.State;

            var state = expandedNode.State;
            while (!state.IsTerminal)
            {
                state.ApplyRandomAction();
            }

            return state;
        }

        private void BackPropagation(Node expandedNode, IStateProtocol finalState)
        {
            _backPropagationStrategy.Update(expandedNode, finalState);
        }
    }
}
