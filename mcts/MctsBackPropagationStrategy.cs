using mcts.normalizer;
using System;

namespace mcts.backpropagation
{
    public interface IBackPropagationStrategy
    {
        INormalizer Normalizer { get; }

        /// <summary>
        /// Update Node này với final state
        /// </summary>
        void Update(Node node, IStateProtocol finalState);
    }

    public class MaximizeReward : IBackPropagationStrategy
    {
        public static readonly uint MAX_PLAYER_COUNT = 4;
        public INormalizer Normalizer { get; }

        public MaximizeReward(INormalizer normalizer)
        {
            Normalizer = normalizer;
        }

        public void Update(Node node, IStateProtocol finalState)
        {
            var rewards = new double[MAX_PLAYER_COUNT];

            for (uint i = 0; i < finalState.PlayerCount; i++)
            {
                rewards[i] = Normalizer.Normalize(finalState.GetReward(i));
            }

            var currentNode = node;
            while (!currentNode.IsRoot)
            {
                var playerIndex = currentNode.PlayerIndex;
                currentNode.AddReward(rewards[playerIndex]);
                currentNode.IncreaseVisitCount();
                currentNode = currentNode.Parent;
            }

            // Root node
            currentNode.IncreaseVisitCount();
        }
    }

    public class MaximizeRewardDifferent : IBackPropagationStrategy
    {
        public static readonly uint MAX_PLAYER_COUNT = 4;
        public INormalizer Normalizer { get; }

        public MaximizeRewardDifferent(INormalizer normalizer)
        {
            Normalizer = normalizer;
        }

        public void Update(Node node, IStateProtocol finalState)
        {
            var rewardDifferences = new double[MAX_PLAYER_COUNT];

            for (uint i = 0; i < finalState.PlayerCount; i++)
            {
                var reward = finalState.GetReward(i);
                var otherPlayerReward = double.MinValue;
                for (uint j = 0; j < finalState.PlayerCount; j++)
                {
                    if (i != j)
                    {
                        otherPlayerReward = Math.Max(otherPlayerReward, finalState.GetReward(j));
                    }
                }

                rewardDifferences[i] = Normalizer.Normalize(reward - otherPlayerReward);
            }

            var currentNode = node;
            while (!currentNode.IsRoot)
            {
                var playerIndex = currentNode.PlayerIndex;
                currentNode.AddReward(rewardDifferences[playerIndex]);
                currentNode.IncreaseVisitCount();
                currentNode = currentNode.Parent;
            }

            // Root node
            currentNode.IncreaseVisitCount();
        }
    }
}
