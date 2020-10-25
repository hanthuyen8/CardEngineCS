using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace engine.vn_poker
{
    public class ActionProtocol : mcts.IActionProtocol
    {
        public enum Type { Null, Sort, Compare }

        private Type _type;

        public ActionProtocol(Type type)
        {
            _type = type;
        }

        public void Dump(string value)
        {
            throw new NotImplementedException();
        }
    }
}
