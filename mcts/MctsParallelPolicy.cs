using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mcts.parallel
{
    public class MajorSelection
    {
        public delegate bool Comparator(IActionProtocol lhs, IActionProtocol rhs);

        private Comparator _comparator;

        public MajorSelection(Comparator comparator)
        {
            _comparator = comparator;
        }

        public IActionProtocol Choose(List<IActionProtocol> actions)
        {
            var counter = new Dictionary<IActionProtocol, uint>();

            foreach (var action in actions)
            {
                ++counter[action];
            }

            IActionProtocol bestAction = null;
            uint best = 0;
            foreach (var elt in counter)
            { 
                if(elt.Value > best || bestAction == null)
                {
                    best = elt.Value;
                    bestAction = elt.Key;
                }
            }

            foreach (var action in actions)
            {
                if (_comparator(action, bestAction))
                {
                    return action;
                }
            }

            return actions[0];
        }
    }
}
