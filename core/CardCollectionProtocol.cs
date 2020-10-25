using System;
using System.Collections.Generic;

namespace engine.core
{
    public abstract class CardCollectionProtocol
    {
        public delegate bool CardComparator(CardProtocol lhs, CardProtocol rhs);
        public delegate bool CardPredicator(CardProtocol card);
        public delegate bool CardEnumerator(CardProtocol card);

        public CardCollection Collection { get; protected set; }
        protected static CardComparator _defaultCardComparator = (CardProtocol lhs, CardProtocol rhs) => lhs.Card < rhs.Card;

        public abstract void AddCard(Card card);
        public abstract void RemoveCard(Card card);
        public abstract void ReArrange(CardComparator comparator);
        public abstract CardProtocol GetCard(uint index);

        public virtual CardProtocol GetCard(Card card)
        {
            for (var i = Collection.Size - 1; i >= 0; i--)
            {
                var protocol = GetCard(i);
                if (protocol.Card == card)
                    return protocol;
            }
            return null;
        }

        public void AddCollection(CardCollection collection)
        {
            var cards = collection.GetAllCards();
            for (int i = 0; i < cards.Count; i++)
            {
                AddCard(cards[i]);
            }
        }

        public void RemoveAllCards()
        {
            var cards = Collection.GetAllCards();
            for (int i = 0; i < cards.Count; i++)
            {
                RemoveCard(cards[i]);
            }
        }

        public void RemoveCollection(CardCollection collection)
        {
            var cards = collection.GetAllCards();
            for (int i = 0; i < cards.Count; i++)
            {
                RemoveCard(cards[i]);
            }
        }

        public void SetCardCollection(CardCollection collection)
        {
            var commonCollection = Collection.Intersect(collection);
            var toBeRemovedCollection = Collection.Differentitate(commonCollection);
            var toBeAddedCollection = collection.Differentitate(commonCollection);

            RemoveCollection(toBeRemovedCollection);
            AddCollection(toBeAddedCollection);
        }

        public List<CardProtocol> GetCards(CardPredicator predicator)
        {
            var result = new List<CardProtocol>();

            for (var i = Collection.Size - 1; i >= 0; i--)
            {
                var card = GetCard(i);
                if(predicator(card))
                {
                    result.Add(card);
                }
            }

            return result;
        }
    }
}
