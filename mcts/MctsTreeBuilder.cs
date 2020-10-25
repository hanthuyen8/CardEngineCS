using mcts.backpropagation;
using mcts.parallel;
using mcts.selection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mcts
{
    public class TreeBuilder
    {
        private MajorSelection _parallelPolicy;
        private NodeCreator _nodeCreator;
        private ISelectionStrategy _selectionStrategy;
        private IBackPropagationStrategy _backPropagationStrategy;
        private ISelectionStrategy _finalSelectionStrategy;

        private int _timeOut;
        private uint _playOuts;
        private uint _threads;
        private uint _cases;

        public TreeBuilder SetParallelPolicy(MajorSelection parallel)
        {
            _parallelPolicy = parallel;
            return this;
        }

        public TreeBuilder SetNodeCreator(NodeCreator creator)
        {
            _nodeCreator = creator;
            return this;
        }

        public TreeBuilder SetSelectionStrategy(ISelectionStrategy selection)
        {
            _selectionStrategy = selection;
            return this;
        }

        public TreeBuilder SetBackPropagation(IBackPropagationStrategy strategy)
        {
            _backPropagationStrategy = strategy;
            return this;
        }

        public TreeBuilder SetFinalSelectionStrategy(ISelectionStrategy selection)
        {
            _finalSelectionStrategy = selection;
            return this;
        }

        public TreeBuilder SetMaxTimeOut(int milliseconds)
        {
            _timeOut = milliseconds;
            return this;
        }

        public TreeBuilder SetMaxPlayOuts(uint playOuts)
        {
            _playOuts = playOuts;
            return this;
        }

        public TreeBuilder SetThreadCount(uint threads)
        {
            _threads = threads;
            return this;
        }

        public TreeBuilder SetSimulationCases(uint cases)
        {
            _cases = cases;
            return this;
        }

        public IActionProtocol ComputeAction(IStateProtocol state, Action<string> logger)
        {
            // FIXME: chưa làm
            return null;
        }
    }
}
