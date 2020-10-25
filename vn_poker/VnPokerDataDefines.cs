using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace engine.vn_poker
{
    public static class Config
    {
        public static readonly uint MAX_CARDS_PER_PLAYER = 13;
        public static readonly uint MAX_HANDS_PER_PLAYER = 3;
        public static readonly uint MAX_PLAYERS = 4;
        public static readonly uint MAX_CARDS_HAND_1 = 5;
        public static readonly uint MAX_CARDS_HAND_2 = 5;
        public static readonly uint MAX_CARDS_HAND_3 = 3;

        public static void SetCardOrder()
        {
            core.Config.SetRankTwoToAceOrder();
            core.Config.SetSuitOrderSpadesToHearts();
        }
    }

    public enum Winner { Player1, Player2, NoWin };

    public enum PokerHand { Hand_1, Hand_2, Hand_3, Hand_Total, Hand_Unknown };
    public enum PokerRank { Rank_Unknown, Rank_1, Rank_2, Rank_3, Rank_4 };
    public enum PokerAceUpDown { Up, Down, None};

    public enum PokerType
    { // So sanh tung chi.
        MissSethand,
        HighCard,
        Pairs,
        TwoPairs,
        ThreeOfAKind,
        Straight_down,
        Straight,
        Flush,
        FullHouse,
        FourOfAKind,
        StraightFlush,

        // Mo rong trong luc so sanh bai.
        ThreeOfAKind_3,
        ThreeOfAKindWithAce_3,
        FullHouse_2,
        FourOfAKindWithAce,
        FourOfAKind_2,
        FourOfAKindWithAce_2,
        StraightFlush_2,
        StraightFlush_1_down, // Thung pha sanh ha. Chi 1.
        StraightFlush_1_up,   // Thung pha sanh thuong. Chi 1.
        StraightFlush_2_down, // Thung pha sanh ha. Chi 2.
        StraightFlush_2_up,   // Thung pha sanh thuong. Chi 2.

        // Win luon khoi phai choi.
        Type_Unknown, // Ranh gioi
        ThreeStraight,
        ThreeFlush,
        SixPairs,
        FivePairsPlushThreeOfAKind,
        SameColor12,
        SameColor13,
        Dragon,
        DragonSameSuit,
        SuperHand, // Danh sap lang.
        LoseThreeHands,

        Win,
        Lose
    };

    public enum CompareSuitType
    {
        NoneCompareSuit,
        HeartsDiamondsClubsSpades, // Cơ > Rô > Chuồng > Bích
        SpadesDiamondsClubsHearts  // Bích > Rô > Chuồng > Cơ
    };
}
