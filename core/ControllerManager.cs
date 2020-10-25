using mcts;
using System;
using System.Collections.Generic;

namespace engine.core
{

    public class ControllerManager
    {
        public static ControllerManager Instance { get; private set; }

        public IControllerProtocol Controller { get; private set; }
        public EventDispatcher EventDispatcher { get; }
        public Scheduler Scheduler { get; }

        private List<ICardPlayerProtocol> _players;

        public ICardPlayerProtocol GetPlayer(int playerIndex)
        {
            return _players[playerIndex];
        }

        public void SetPlayer(int playerIndex, ICardPlayerProtocol player)
        {
            if (player != null)
            {
                _players[playerIndex] = player;
            }
        }

        public void SetController(IControllerProtocol controller)
        {
            Controller = controller;
        }

        public void ResetPlayer(int playerIndex, ICardPlayerProtocol player)
        {
            if (_players[playerIndex] != null && _players[playerIndex] == player)
            {
                _players[playerIndex] = null;
            }
        }

        private ControllerManager()
        { 
        }
    }
}
