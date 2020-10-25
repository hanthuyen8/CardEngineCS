using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// Monte Carlo Tree Search Algorithm
/// </summary>
namespace mcts
{
    /// <summary>
    /// Action được dùng để chuyển đổi giữa các State
    /// </summary>
    public interface IActionProtocol
    {
        void Dump(string value);
    }

    public interface IStateProtocol
    {
        /// <summary>
        /// State này đã kết thúc chưa? Vd: End Game.
        /// </summary>
        bool IsTerminal { get; }
        uint PlayerCount { get; }

        /// <summary>
        /// Ai đã đưa ra hành động cuối cùng ?
        /// </summary>
        uint GetLastPlayerIndex();

        /// <summary>
        /// Ai tiếp theo sẽ hành động?
        /// </summary>
        uint GetNextPlayerIndex();

        /// <summary>
        /// Lấy ra số tiền thưởng cho Player bất kỳ
        /// </summary>
        /// <param name="playerIndex">Player cần truy vấn</param>
        double GetReward(uint playerIndex);

        void ApplyRandomAction();

        /// <summary>
        /// Lấy ra action cuối cùng được thực hiện
        /// </summary>
        IActionProtocol GetLastAction();

        /// <summary>
        /// Áp dụng action nhất định vào State này
        /// </summary>
        /// <param name="action">action cần được apply</param>
        void ApplyAction(IActionProtocol action);

        /// <summary>
        /// Lấy ra tất cả actions hợp lệ từ State này
        /// </summary>
        List<IActionProtocol> GetActions();
    }
}
