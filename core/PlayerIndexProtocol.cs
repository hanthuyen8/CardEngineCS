using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace engine.core
{
    public class PlayerIndexProtocol
    {
        public uint PlayerIndex { get; private set; } = uint.MaxValue;
        public virtual void SetPlayerIndex(uint playerIndex)
        {
            PlayerIndex = playerIndex;
        }

        public virtual void ResetPlayerIndex()
        {
            PlayerIndex = uint.MaxValue;
        }
    }
}
