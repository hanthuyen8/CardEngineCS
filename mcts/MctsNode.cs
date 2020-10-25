using engine;
using System;
using System.Collections.Generic;

/// <summary>
/// Monte Carlo Tree Search Algorithm
/// </summary>
namespace mcts
{
    public abstract class NodeBase
    {
        /// <summary>
        /// Kiểm tra Node này đã được expanded hết mức chưa?
        /// </summary>
        public abstract bool IsExpanded { get; }

        /// <summary>
        /// Lấy ra Action đã tạo ra Node này
        /// </summary>
        public abstract IActionProtocol Action { get; protected set; }

        /// <summary>
        /// Lấy ra State được liên kết với Node này
        /// </summary>
        public abstract IStateProtocol State { get; protected set; }

        public abstract int ChildrenCount { get; }

        /// <summary>
        /// Expand Node này và trả về Child Node vừa được tạo ra.
        /// Điều kiện: IsExpanded phải == true
        /// </summary>
        public abstract NodeBase Expand();
        
        public abstract NodeBase GetChild(int index);

        /// <summary>
        /// Node này có phải là Node Top-most (không có parent)
        /// </summary>
        public bool IsRoot { get => Parent == null; }

        /// <summary>
        /// State của Node này đã kết thúc chưa? Vd: End Game
        /// </summary>
        public bool IsTerminal { get => this.State.IsTerminal; }

        /// <summary>
        /// Node này do ai tạo ra?
        /// </summary>
        public uint PlayerIndex { get; }

        protected NodeBase Parent { get; }

        /// <summary>
        /// Số lần Node này đã được duyệt qua
        /// </summary>
        public uint VisitsCount { get; private set; } = 0;

        public double Reward { get; private set; } = 0;

        /// <summary>
        /// Lấy ra Reward/Visit count ratio
        /// </summary>
        public double WinRate
        {
            get
            {
                if (VisitsCount == 0)
                    return 0;

                return Reward / VisitsCount;
            }
        }

        protected NodeBase(NodeBase parent, uint playerIndex)
        {
            Parent = parent;
            PlayerIndex = playerIndex;
        }

        public void AddReward(double reward)
        {
            Reward += reward;
        }

        public void IncreaseVisitCount()
        {
            VisitsCount++;
        }

        /// <summary>
        /// Lấy ra tổng số Child Node trong Subtree này (Debug)
        /// </summary>
        public int GetTotalChildrenCount()
        {
            int result = 1;
            for (var i = ChildrenCount - 1; i >= 0; i--)
            {
                var child = GetChild(i);
                result += child.GetTotalChildrenCount();
            }

            return result;
        }

        /// <summary>
        /// Đệ quy tìm max children ( Debug)
        /// </summary>
        public int GetMaxChildrenCount()
        {
            var result = ChildrenCount;
            for (var i = result - 1; i >= 0; i--)
            {
                var child = GetChild(i);
                result = Math.Max(result, child.GetMaxChildrenCount());
            }

            return result;
        }
    }

    sealed public class Node : NodeBase
    {
        new public Node Parent => (Node)base.Parent;

        public override IActionProtocol Action { get; protected set; }
        public override IStateProtocol State { get; protected set; }

        public override bool IsExpanded
        {
            get
            {
                return _children.Count != 0 && _children.Count == _actions.Count;
            }
        }

        public override int ChildrenCount => _children.Count;

        private List<IActionProtocol> _actions;
        private List<Node> _children;

        public static Node CreateRootNode(uint playerIndex, IActionProtocol action, IStateProtocol state)
        {
            return new Node(null, playerIndex, action, state);
        }

        public static Node CreateNode(Node parent, uint playerIndex, IActionProtocol action, IStateProtocol state)
        {
            return new Node(parent, playerIndex, action, state);
        }

        private Node(NodeBase parent, uint playerIndex, IActionProtocol action, IStateProtocol state) : base(parent, playerIndex)
        {
            Action = action;
            State = state;
        }

        public Node SelectBestChild(System.Func<Node, double> evaluator)
        {
            Node bestChild = null;
            var bestValue = double.MinValue;

            for (var i = 0; i < _children.Count; i++)
            {
                var child = (Node)GetChild(i);
                var value = evaluator(child);
                if (value > bestValue)
                {
                    bestValue = value;
                    bestChild = child;
                }
            }

            return bestChild;
        }

        /// <summary>
        /// WARNING: Hàm này có thể không đúng
        /// </summary>
        public override NodeBase Expand()
        {
            // FIXME: Assert IsExpanded == false

            if (_actions.Count == 0)
            {
                _actions = State.GetActions();
                _children = new List<Node>();

                _actions.Shuffle();
            }

            // FIXME: Assert _actions is not empty
            var nextAction = _actions[_children.Count];
            var nextState = State;
            var nextPlayerIndex = State.GetNextPlayerIndex();
            nextState.ApplyAction(nextAction);
            var child = CreateNode(this, nextPlayerIndex, nextAction, nextState);
            _children.Add(child);
            return child;
        }

        public override NodeBase GetChild(int index)
        {
            return _children[index];
        }
    }

    public class NodeCreator
    {
        public Node CreateNode(IStateProtocol state)
        {
            var action = state.GetLastAction();
            var playerIndex = state.GetLastPlayerIndex();
            return Node.CreateRootNode(playerIndex, action, state);
        }
    }
}
