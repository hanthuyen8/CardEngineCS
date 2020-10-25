using mcts;
using System;
using System.Threading.Tasks;
using System.Threading;

namespace engine.core
{
    using ComputationCallback = Func<mcts.IStateProtocol, mcts.IActionProtocol>;

    sealed public class GenericAi
    {
        public bool IsComputing => _future?.Status == TaskStatus.Running;
        public long ElapsedTime
        {
            get
            {
                var now = DateTime.Now.Millisecond;
                return now - _beginPoint;
            }
        }

        private long _beginPoint;
        ComputationCallback _callback;
        Task<mcts.IActionProtocol> _future;

        public GenericAi(ComputationCallback callback)
        {
            _callback = callback;
            _beginPoint = 0;
        }

        /// <summary>
        /// Tính toán hành động tiếp theo cho State này
        /// </summary>
        public void ComputeMove(IStateProtocol state)
        {
            if (IsComputing)
                return;

            _beginPoint = DateTime.Now.Millisecond;
            Func<mcts.IActionProtocol> act = () => {
                return _callback?.Invoke(state);
            };
            _future = new Task<mcts.IActionProtocol>(act);
            _future.Start();
        }

        /// <summary>
        /// Lấy ra kết quả đã tính toán
        /// </summary>
        /// <returns></returns>
        public mcts.IActionProtocol GetComputeMove()
        {
            if (IsComputing)
                return null;

            var result = _future.Result;
            return result;
        }
    }
}
