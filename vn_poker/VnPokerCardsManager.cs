using engine.core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace engine.vn_poker
{
    public class CardsManager
    {
        public static CardsManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new CardsManager();
                    _instance.SetCompareSuitType(CompareSuitType.NoneCompareSuit);
                }
                return _instance;
            }
        }
        public PokerMetaData MetaData { get; private set; } = new PokerMetaData();

        private static CardsManager _instance;
        private CompareSuitType _compareSuitType;

        /// <summary>
        /// Chia bài
        /// </summary>
        public void PrepareForNewGame()
        {
            Reset();
            Config.SetCardOrder();

            var cards = RandomCards();

            // Set cards to players
            int cardsIndex = 0;

            for (uint playerId = 0; playerId < Config.MAX_PLAYERS; playerId++)
            {
                for (uint i = 0; i < Config.MAX_CARDS_PER_PLAYER; i++)
                {
                    MetaData.cards[playerId].cardTotal.Insert(cards[cardsIndex++]);
                }
            }
        }

        /// <summary>
        /// Client gửi dữ liệu
        /// </summary>
        public bool SetHandData(uint playerId, core.CardCollection hand1, core.CardCollection hand2, core.CardCollection hand3)
        {
            var cards = new CardCollection(0);
            cards.Insert(hand1);
            cards.Insert(hand2);
            cards.Insert(hand3);

            if (cards.Mask != MetaData.cards[playerId].cardTotal.Mask)
            {
                Console.WriteLine("Wrong Data");
                return false;
            }

            MetaData.cards[playerId].cardByHand[0] = hand1;
            MetaData.cards[playerId].cardByHand[1] = hand2;
            MetaData.cards[playerId].cardByHand[2] = hand3;

            UpdateHandType(playerId);
            return true;
        }

        public void Compare()
        { }

        public PokerResult GetPokerResult(uint playerId)
        {
            var result = MetaData.results[playerId];
            return result;
        }

        public void ManagerSendPlayerData(uint playerId, PokerCards data)
        {
            if (playerId < Config.MAX_PLAYERS)
            {
                MetaData.cards[playerId] = data;
            }
        }

        public void Reset()
        {
            MetaData.Reset();
        }

        public void SetCompareSuitType(CompareSuitType type)
        {
            _compareSuitType = type;
        }


        private void UpdateHandType(uint playerId)
        {
            var cleanSweepHands =
                Checker.GetCleanSweepHandType(MetaData.cards[playerId].cardTotal, out CardCollection card);

            if (cleanSweepHands != PokerType.Type_Unknown)
            {
                MetaData.results[playerId].cleanSweepResult.type = cleanSweepHands;
            }
            else
            {
                var isMissHand = Checker.CheckMissSetHand(
                    MetaData.cards[playerId].cardByHand[0],
                    MetaData.cards[playerId].cardByHand[1],
                    MetaData.cards[playerId].cardByHand[2], _compareSuitType);
                if (isMissHand)
                {
                    MetaData.results[playerId].isMissSetHand = true;
                    MetaData.results[playerId].handResult[0].type = PokerType.MissSethand;
                    MetaData.results[playerId].handResult[1].type = PokerType.MissSethand;
                    MetaData.results[playerId].handResult[2].type = PokerType.MissSethand;
                }
                else
                {
                    MetaData.results[playerId].isMissSetHand = false;
                    MetaData.results[playerId].handResult[0].type = Checker.GetHandType(
                        MetaData.cards[playerId].cardByHand[0], out _, PokerHand.Hand_1);

                    MetaData.results[playerId].handResult[1].type = Checker.GetHandType(
                        MetaData.cards[playerId].cardByHand[1], out _, PokerHand.Hand_2);

                    MetaData.results[playerId].handResult[2].type = Checker.GetHandType(
                        MetaData.cards[playerId].cardByHand[2], out _, PokerHand.Hand_3);
                }
            }
        }

        private List<core.Card> RandomCards()
        {
            var cards = new List<core.Card>();
            for (var rank = core.CardRank.Two; rank <= core.CardRank.Ace; rank++)
            {
                for (var suit = core.CardSuit.Spades; suit <= core.CardSuit.Hearts; suit++)
                {
                    cards.Add(new core.Card(rank, suit));
                }
            }

            cards.Shuffle();
            return cards;
        }

        private bool CompareCleanSweepHand(PokerResult result1, PokerResult result2, PokerCards cards1, PokerCards cards2)
        {
            if (result1.cleanSweepResult.type >= PokerType.ThreeStraight ||
                result2.cleanSweepResult.type >= PokerType.ThreeStraight)
            {
                if (result1.cleanSweepResult.type == result2.cleanSweepResult.type)
                {
                    Checker.GetCleanSweepHandType(cards1.cardTotal, out CardCollection cleanSweep1);
                    Checker.GetCleanSweepHandType(cards2.cardTotal, out CardCollection cleanSweep2);

                    var cmpResult = Checker.CmpCards(cleanSweep1, cleanSweep2, _compareSuitType);
                    if (cmpResult != Winner.NoWin)
                    {
                        if (cmpResult == Winner.Player1)
                            result2.cleanSweepResult.rank--;
                        else
                            result1.cleanSweepResult.rank--;
                    }
                }
                else if (result1.cleanSweepResult.type < result2.cleanSweepResult.type)
                {
                    result1.cleanSweepResult.rank--;
                }
                else
                {
                    result2.cleanSweepResult.rank--;
                }
                return true;
            }
            return false;
        }

        private void CompareNormalHand(PokerResult result1, PokerResult result2, PokerCards cards1, PokerCards cards2)
        {
            // FIXME: chưa làm
        }
    }
}
