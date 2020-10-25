using mcts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace engine.core
{
    public class StateLeaf
    {
        public bool CanUndo => _previousLeaf != null;
        public bool CanRedo => _nextLeaf != null;
        public bool HasState => State != null;
        public mcts.IStateProtocol State { get; }

        private StateLeaf _previousLeaf;
        private StateLeaf _nextLeaf;

        public StateLeaf Undo()
        {
            return _previousLeaf;
        }

        public StateLeaf Redo()
        {
            return _nextLeaf;
        }

        public StateLeaf(mcts.IStateProtocol state)
        {
            State = state;
        }

        public StateLeaf ProcessCommand(IStateCommandProtocol command, List<StateEvent> events)
        {
            var nextState = command.Process(State, events);
            var next = new StateLeaf(nextState);
            next._previousLeaf = this;
            this._nextLeaf = next;
            return next;
        }

        public bool CanProcessCommand(IStateCommandProtocol command)
        {
            return command.CanProcess(State);
        }
    }
}
