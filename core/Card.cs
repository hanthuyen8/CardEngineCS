using System;
using System.Collections.Generic;
using System.Text;

namespace engine
{
    namespace core
    {
        sealed public class Card
        {
            public uint Index { get; }
            public ulong Mask { get => 1ul << (int)Index; }
            public CardSuit Suit { get => new CardSuit(Index % Config.CARD_SUIT_COUNT); }
            public CardRank Rank { get => new CardRank(Index / Config.CARD_SUIT_COUNT); }

            public Card(uint index = uint.MaxValue)
            {
                Index = index;
            }

            public Card(CardRank rank, CardSuit suit)
            {
                Index = (rank.Index * Config.CARD_SUIT_COUNT) + suit.Index;
            }

            #region Operators
            public static bool operator ==(Card lhs, Card rhs)
            {
                return lhs.Index == rhs.Index;
            }

            public static bool operator !=(Card lhs, Card rhs)
            {
                return lhs.Index != rhs.Index;
            }

            public static bool operator <(Card lhs, Card rhs)
            {
                return lhs.Index < rhs.Index;
            }

            public static bool operator >(Card lhs, Card rhs)
            {
                return lhs.Index > rhs.Index;
            }

            public static uint operator -(Card lhs, uint n)
            {
                var value = lhs.Index - n;
                return value > 0 ? value : 0;
            }
            #endregion
        }
    }
}
