namespace engine.vn_poker
{
    public class PokerHandResult
    {
        public PokerRank rank;
        public PokerType type;

        public void Reset()
        {
            rank = PokerRank.Rank_4;
            type = PokerType.Type_Unknown;
        }
    };

    public class PokerResult
    {
        public bool isMissSetHand;               // Xu ly truong hop Binh Lung.
        public PokerHandResult cleanSweepResult; // Xu ly CleanSweepHand.
        public PokerHandResult[] handResult = new PokerHandResult[Config.MAX_HANDS_PER_PLAYER];

        public void Reset()
        {
            isMissSetHand = false;
            cleanSweepResult.Reset();

            for (var i = 0; i < Config.MAX_HANDS_PER_PLAYER; i++)
            {
                handResult[i].Reset();
            }
        }
    };

    public class PokerCards
    {
        // Tat ca 13 la bai.
        public core.CardCollection cardTotal;
        // Cards tren tung chi.
        public core.CardCollection[] cardByHand = new core.CardCollection[Config.MAX_HANDS_PER_PLAYER];

        public void Reset()
        {
            cardTotal.SetMask(0);

            for (var i = 0; i < Config.MAX_HANDS_PER_PLAYER; i++)
            {
                cardByHand[i].SetMask(0);
            }
        }
    };

    public class PokerMetaData
    {
        // Luu tru card cho tung player.
        public PokerCards[] cards = new PokerCards[Config.MAX_PLAYERS]; 
        public PokerResult[] results= new PokerResult[Config.MAX_PLAYERS];

        public void Reset()
        {
            for (var i = 0; i < Config.MAX_PLAYERS; i++)
            {
                cards[i].Reset();
                results[i].Reset();
            }
        }
    };
}
