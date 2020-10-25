using System;

namespace engine
{
    namespace core
    {
        sealed public class CardSuit
        {
            public static readonly CardSuit Spades = new CardSuit(0);
            public static readonly CardSuit Clubs = new CardSuit(0);
            public static readonly CardSuit Diamonds = new CardSuit(0);
            public static readonly CardSuit Hearts = new CardSuit(0);

            #region Operators
            public static bool operator ==(CardSuit lhs, CardSuit rhs)
            {
                return lhs.Index == rhs.Index;
            }

            public static bool operator !=(CardSuit lhs, CardSuit rhs)
            {
                return lhs.Index != rhs.Index;
            }

            public static bool operator <(CardSuit lhs, CardSuit rhs)
            {
                return lhs.Index < rhs.Index;
            }

            public static bool operator <=(CardSuit lhs, CardSuit rhs)
            {
                return lhs.Index <= rhs.Index;
            }

            public static bool operator >(CardSuit lhs, CardSuit rhs)
            {
                return lhs.Index > rhs.Index;
            }

            public static bool operator >=(CardSuit lhs, CardSuit rhs)
            {
                return lhs.Index >= rhs.Index;
            }

            public static CardSuit operator ++(CardSuit rank)
            {
                ++rank.Index;
                return rank;
            }

            public static CardSuit operator --(CardSuit rank)
            {
                --rank.Index;
                return rank;
            }

            public static CardSuit operator +(CardSuit lhs, uint n)
            {
                return new CardSuit(lhs.Index + n);
            }

            public static CardSuit operator -(CardSuit lhs, uint n)
            {
                var value = lhs.Index - n;
                value = value > 0 ? value : 0;
                return new CardSuit(value);
            }
            #endregion

            public uint Index { get; private set; }
            public ulong Mask
            {
                get
                {
                    const ulong lowestMask = 1;
                    const ulong fullSuitMask =
                        (lowestMask << 0) | (lowestMask << 4) | (lowestMask << 8) | (lowestMask << 12) |
                        (lowestMask << 16) | (lowestMask << 20) | (lowestMask << 24) | (lowestMask << 28) |
                        (lowestMask << 40) | (lowestMask << 44) | (lowestMask << 48);

                    // FIXME: add test here
                    // fullSuitMask == 0b0001'0001'0001'0001'0001'0001'0001'0001'0001'0001'0001'0001'0001;

                    return fullSuitMask << (int)Index;
                }
            }

            public CardSuit(uint index)
            {
                Index = index;
            }

            public static void SetSuitOrderSpadesToHearts(params uint[] order)
            {
                Spades.Index = order[0];
                Clubs.Index = order[1];
                Diamonds.Index = order[2];
                Hearts.Index = order[3];
            }
        }
    }
}
