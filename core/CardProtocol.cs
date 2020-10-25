using System;
using System.Collections.Generic;
using System.Text;

namespace engine
{
    namespace core
    {
        public enum CardState
        { 
            FaceDown, FaceUp
        }

        public class CardProtocol {

            public bool HasCard { get => Card != null; }
            public Card Card { get; private set; }
            public CardState State { get; private set; } = CardState.FaceDown;

            public void SetCard(Card card)
            {
                this.Card = card;
                OnCardChanged();
            }

            public void SetState(CardState state)
            {
                this.State = state;
                OnCardStateChanged();
            }

            protected virtual void OnCardChanged()
            { }

            protected virtual void OnCardStateChanged()
            { }
        }
    }
}
