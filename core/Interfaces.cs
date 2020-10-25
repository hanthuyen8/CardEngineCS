using mcts;
using System;
using System.Collections.Generic;

namespace engine.core
{
    public interface ICardDeckProtocol
    {
        /// <summary>
        /// Chia bài đến tất cả Players
        /// </summary>
        /// <param name="callback">Callback sẽ được gọi khi đã chia bài xong hết</param>
        /// <param name="firstPlayerIndex">Player đầu tiên được chia</param>
        /// <param name="collections">Tất cả bài cần chia</param>
        void DealCards(Action callback, uint firstPlayerIndex, CardCollection[] collections);
    }

    public interface ICardPlayerProtocol
    {
        /// <summary>
        /// Bắt đầu chia bài
        /// </summary>
        void OnDealingCardStarted();

        /// <summary>
        /// Chia bài xong
        /// </summary>
        void OnDealingCardFinished();

        /// <summary>
        /// Một lá bài đã được chia
        /// </summary>
        /// <param name="playerIndex">Player nhận được lá đó</param>
        /// <param name="card">Card đã được chia</param>
        void OnCardDealt(uint playerIndex, Card card);

    }

    public interface IStateCommandProtocol
    {
        mcts.IStateProtocol Process(mcts.IStateProtocol state, List<StateEvent> events);
        bool CanProcess(mcts.IStateProtocol state);
    }

    public interface IControllerProtocol
    {
        bool IsPaused { get; }
        bool CanUndo { get; }
        bool CanRedo { get; }
        bool CanStep { get; }

        void NewGame();
        void ContinueGame();
        void LoadGame(mcts.IStateProtocol state);
        void Resume();
        void Pause();
        void Undo();
        void Redo();
        void Step();
        void Refresh();

        mcts.IStateProtocol GetCurrentState();
        void ProcessCommand(IStateCommandProtocol command);
        bool CanProcessCommand(IStateCommandProtocol command);
        void Think(uint playerIndex, long thinkingDuration, long waitingDuration);

    }
}
