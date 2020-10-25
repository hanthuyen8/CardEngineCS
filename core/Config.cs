using engine.core;
using System.Collections.Generic;
using System;
using System.Linq;

namespace engine.core
{
    public static class Config
    {
        public static readonly uint CARD_SUIT_COUNT = 4;
        public static readonly uint CARD_RANK_COUND = 13;
        public static readonly uint CARD_COUNT = CARD_SUIT_COUNT * CARD_RANK_COUND;
        public static readonly uint PLAYER_COUNT = 4;

        public static void SetRankAceToKingOrder()
        {
            CardRank.SetRankOrderAceToKing(0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12);
        }

        public static void SetRankThreeToTwoOrder()
        {
            CardRank.SetRankOrderAceToKing(11, 12, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10);
        }

        public static void SetRankTwoToAceOrder()
        {
            CardRank.SetRankOrderAceToKing(12, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11);
        }

        public static void SetSuitOrderSpadesToHearts()
        {
            CardSuit.SetSuitOrderSpadesToHearts(0, 1, 2, 3);
        }

        public static void SetSuitOrderSpadesDiamondsClubsHearts()
        {
            CardSuit.SetSuitOrderSpadesToHearts(0, 2, 1, 3);
        }

        public static void SetSuitOrderDiamondsClubsHeartsSpades()
        {
            CardSuit.SetSuitOrderSpadesToHearts(3, 1, 0, 2);
        }
    }

    public static class Utility
    {

    }

    public static class EnumerableExtension
    {
        private static Random globalRng = new Random();

        [ThreadStatic]
        private static Random _rng;

        private static Random rng
        {
            get
            {
                if (_rng == null)
                {
                    int seed;
                    lock (globalRng)
                    {
                        seed = globalRng.Next();
                    }
                    _rng = new Random(seed);
                }
                return _rng;
            }
        }

        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> items)
        {
            return items.OrderBy(i => rng.Next());
        }
    }
}
