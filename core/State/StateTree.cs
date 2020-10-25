using mcts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace engine.core
{

    public class StateTree
    {
        public bool CanUndo => _currentLeaf.CanUndo;
        public bool CanRedo => _currentLeaf.CanRedo;
        public bool HasState => _currentLeaf.HasState;

        public IStateProtocol CurrentState => _currentLeaf.State;

        private StateLeaf _root;
        private StateLeaf _currentLeaf;

        public StateTree(IStateProtocol state)
        {
            _root = new StateLeaf(state);
            _currentLeaf = _root;
        }

        public bool Redo()
        {
            if (CanRedo)
            {
                _currentLeaf = _currentLeaf.Redo();
                return true;
            }
            return false;
        }

        public bool Undo()
        {
            if (CanUndo)
            {
                _currentLeaf = _currentLeaf.Undo();
                return true;
            }
            return false;
        }

        public List<StateEvent> ProcessCommand(IStateCommandProtocol command)
        {
            var ev = new List<StateEvent>();
            _currentLeaf = _currentLeaf.ProcessCommand(command, ev);
            return ev;
        }

        public bool CanProcessCommand(IStateCommandProtocol command)
        {
            return _currentLeaf.CanProcessCommand(command);
        }

        
    }
}
