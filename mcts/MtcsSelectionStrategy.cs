using System;

namespace mcts.selection
{
    public interface ISelectionStrategy
    {
        /// <summary>
        /// Select best child của Node này
        /// </summary>
        Node Select(Node node);
    }

    /// <summary>
    /// Select child với Win Rate cao nhất
    /// </summary>
    sealed public class SelectMaxChild : ISelectionStrategy
    {
        public Node Select(Node node)
        {
            return node.SelectBestChild((child) => child.WinRate);
        }
    }

    /// <summary>
    /// Select child với Visits Count cao nhất
    /// </summary>
    sealed public class SelectRobustChild : ISelectionStrategy
    {
        public Node Select(Node node)
        {
            return node.SelectBestChild((child) => child.VisitsCount);
        }
    }

    /// <summary>
    /// Select child với lower confidence lớn nhất
    /// </summary>
    sealed public class SelectSecureChild : ISelectionStrategy
    {
        private double _a;

        public SelectSecureChild(double a = 1)
        {
            _a = a;
        }

        public Node Select(Node node)
        {
            return node.SelectBestChild((child) => {
                var visit = child.VisitsCount;
                var value = child.Reward + _a * Math.Sqrt(visit);
                return value;
            });
        }
    }

    /// <summary>
    /// Upper confidence bound algorithm.
    /// </summary>
    public class UpperConfidenceBound : ISelectionStrategy
    {
        protected double _c;

        public UpperConfidenceBound(double c = 2)
        {
            _c = c;
        }

        public Node Select(Node node)
        {
            if (!node.IsExpanded)
            {
                return (Node)node.Expand();
            }

            var logValue = Math.Log(node.VisitsCount);

            return node.SelectBestChild((child) => {
                var visit = child.VisitsCount;
                var inversedVisit = 1.0 / visit;
                var exploit = child.Reward * inversedVisit;
                var explore = Math.Sqrt(_c * logValue * inversedVisit);
                var value = exploit + explore;
                return value;
            });
        }
    }
}
