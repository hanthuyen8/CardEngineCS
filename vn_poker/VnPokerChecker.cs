using engine.core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace engine.vn_poker
{
    public static class Checker
    {
        private static int[] HeartsDiamondsClubsSpadesSuit = new int[4] { 0, 1, 2, 3 };
        private static int[] SpadesDiamondsClubsHeartsSuit = new int[4] { 3, 1, 2, 0 };

        public static int GetScoreBySuit(CardSuit suit, CompareSuitType type)
        {
            switch (type)
            {
                case CompareSuitType.NoneCompareSuit:
                    return 0;
                case CompareSuitType.HeartsDiamondsClubsSpades:
                    return HeartsDiamondsClubsSpadesSuit[(int)suit.Index];
                case CompareSuitType.SpadesDiamondsClubsHearts:
                    return SpadesDiamondsClubsHeartsSuit[(int)suit.Index];
            }
            return 0;
        }

        public static Winner CmpCard(Card card1, Card card2, CompareSuitType type)
        {
            if (card1.Rank == card2.Rank)
            {
                switch (type)
                {
                    case CompareSuitType.NoneCompareSuit:
                        return Winner.NoWin;
                    default:
                        var suitScore1 = GetScoreBySuit(card1.Suit, type);
                        var suitScore2 = GetScoreBySuit(card2.Suit, type);
                        return suitScore1 > suitScore2 ? Winner.Player1 : Winner.Player2;
                }
            }
            else
            {
                return card1.Rank > card2.Rank ? Winner.Player1 : Winner.Player2;
            }
        }

        public static Winner CmpCards(CardCollection cards1, CardCollection cards2, CompareSuitType type)
        {
            // FIXME: assert cards1.size == cards2.size

            var size = cards1.Size;
            if (size == 0)
                return Winner.NoWin;

            Winner cmp(CompareSuitType t)
            {
                for (uint cardIndex = size - 1; cardIndex >= 0; cardIndex--)
                {
                    var card1 = cards1.At(cardIndex);
                    var card2 = cards2.At(cardIndex);
                    var result = CmpCard(card1, card2, t);
                    if (result != Winner.NoWin)
                        return result;
                }
                return Winner.NoWin;
            }

            var r = cmp(CompareSuitType.NoneCompareSuit);
            if (r != Winner.NoWin)
                return r;

            return cmp(type);
        }

        public static Winner CmpCardsUnknownHands(CardCollection cards1, CardCollection cards2, CompareSuitType type)
        {
            var size = Math.Min(cards1.Size, cards2.Size);
            if (size == 0)
                return Winner.NoWin;

            for (uint cardIndex = size - 1; cardIndex >= 0; cardIndex--)
            {
                var card1 = cards1.At(cardIndex);
                var card2 = cards2.At(cardIndex);
                var result = CmpCard(card1, card2, type);
                if (result != Winner.NoWin)
                    return result;
            }

            if (cards1.Size == cards2.Size)
                return Winner.NoWin;

            return cards1.Size > cards2.Size ? Winner.Player1 : Winner.Player2;
        }

        /// <summary>
        /// So sánh cù lũ
        /// </summary>
        public static Winner CmpFullHouses(CardCollection cards1, CardCollection cards2, CompareSuitType type)
        {
            return Winner.NoWin;
        }

        /// <summary>
        /// So sánh 2 chi
        /// </summary>
        public static Winner CmpHands(CardCollection hand1, CardCollection hand2, PokerHand hand, CompareSuitType type)
        {
            return Winner.NoWin;
        }

        public static bool CheckSameSuit(CardCollection input, out CardCollection output, uint count, bool returnAll = false)
        {
            output = input;
            return false;
        }

        public static bool CheckSameRank(CardCollection input, out CardCollection output, uint count, bool strongFirst = true)
        {
            output = input;
            return false;
        }

        public static bool CheckSameColor(CardCollection input, out CardCollection output, uint count)
        {
            output = input;
            return false;
        }

        public static bool CheckStraight(CardCollection input, out CardCollection output, uint count)
        {
            output = input;
            return false;
        }

        //

        public static bool CheckDragonSameSuit(CardCollection input, out CardCollection output)
        {
            output = input;
            return false;
        }
        public static bool CheckFivePairsAndThreeOfAKind(CardCollection input, out CardCollection output)
        {
            output = input;
            return false;
        }
        public static bool CheckSixPairs(CardCollection input, out CardCollection output)
        {
            output = input;
            return false;
        }
        public static bool CheckThreeSameSuit(CardCollection input, out CardCollection output)
        {
            output = input;
            return false;
        }
        public static bool CheckThreeStraight(CardCollection input, out CardCollection output)
        {
            output = input;
            return false;
        }
        public static PokerType GetCleanSweepHandType(CardCollection input, out CardCollection output)
        {
            output = input;
            return PokerType.Type_Unknown;
        }

        //
        public static bool CheckStraightFlush(CardCollection input, out CardCollection output)
        {
            output = input;
            return false;
        }
        public static bool CheckFullHouse(CardCollection input, out CardCollection output)
        {
            output = input;
            return false;
        }
        public static bool CheckTwoPairs(CardCollection input, out CardCollection output)
        {
            output = input;
            return false;
        }
        public static bool CheckMissSetHand(CardCollection hand1, CardCollection hand2, CardCollection hand3, CompareSuitType type)
        {
            return false;
        }
        public static PokerAceUpDown CheckAceUpDownNone(CardCollection input)
        {
            return PokerAceUpDown.None;
        }

        //

        public static PokerType GetHandType(CardCollection input, out CardCollection output, PokerHand hand)
        {
            output = input;
            return PokerType.Type_Unknown;
        }
        public static void FindHighCard(CardCollection input, out CardCollection output, uint count = 1)
        {
            output = input;
        }
        public static void FindMinCard(CardCollection input, out CardCollection output, uint count = 1)
        {
            output = input;
        }
    }
}
