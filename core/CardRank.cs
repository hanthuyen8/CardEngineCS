using System;
using System.Collections.Generic;
using System.Text;

namespace engine
{
    namespace core
    {
        sealed public class CardRank
        {
            public static readonly CardRank Ace = new CardRank(0);
            public static readonly CardRank Two = new CardRank(1);
            public static readonly CardRank Three = new CardRank(2);
            public static readonly CardRank Four = new CardRank(3);
            public static readonly CardRank Five = new CardRank(4);
            public static readonly CardRank Six = new CardRank(5);
            public static readonly CardRank Seven = new CardRank(6);
            public static readonly CardRank Eight = new CardRank(7);
            public static readonly CardRank Nine = new CardRank(8);
            public static readonly CardRank Ten = new CardRank(9);
            public static readonly CardRank Jack = new CardRank(10);
            public static readonly CardRank Queen = new CardRank(11);
            public static readonly CardRank King = new CardRank(12);

            #region Operators
            public static bool operator ==(CardRank lhs, CardRank rhs)
            {
                return lhs.Index == rhs.Index;
            }

            public static bool operator !=(CardRank lhs, CardRank rhs)
            {
                return lhs.Index != rhs.Index;
            }

            public static bool operator <(CardRank lhs, CardRank rhs)
            {
                return lhs.Index < rhs.Index;
            }

            public static bool operator <=(CardRank lhs, CardRank rhs)
            {
                return lhs.Index <= rhs.Index;
            }

            public static bool operator >(CardRank lhs, CardRank rhs)
            {
                return lhs.Index > rhs.Index;
            }

            public static bool operator >=(CardRank lhs, CardRank rhs)
            {
                return lhs.Index >= rhs.Index;
            }

            public static CardRank operator ++(CardRank rank)
            {
                ++rank.Index;
                return rank;
            }

            public static CardRank operator --(CardRank rank)
            {
                --rank.Index;
                return rank;
            }

            public static CardRank operator +(CardRank lhs, uint n)
            {
                return new CardRank(lhs.Index + n);
            }

            public static uint operator -(CardRank lhs, uint n)
            {
                var value = lhs.Index - n;
                return value > 0 ? value : 0;
            }
            #endregion

            public uint Index { get; private set; }
            public ulong Mask
            {
                get
                {
                    const ulong fullRankMask = 0b1111;
                    return fullRankMask << (int)(Index * Config.CARD_SUIT_COUNT);
                }
            }

            public CardRank(uint index)
            {
                Index = index;
            }

            public static void SetRankOrderAceToKing(params uint[] ranks)
            {
                Ace.Index = ranks[0];
                Two.Index = ranks[1];
                Three.Index = ranks[2];
                Four.Index = ranks[3];
                Five.Index = ranks[4];
                Six.Index = ranks[5];
                Seven.Index = ranks[6];
                Eight.Index = ranks[7];
                Nine.Index = ranks[8];
                Ten.Index = ranks[9];
                Jack.Index = ranks[10];
                Queen.Index = ranks[11];
                King.Index = ranks[12];
            }
        }
    }
}
