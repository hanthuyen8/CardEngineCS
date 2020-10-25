using mcts;
using System;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;

namespace engine.core
{
    using ComputationCallback = Action<mcts.IActionProtocol>;

    sealed public class GenericAiPack
    {
        private struct Computation
        {
            public bool shouldStop;
            public long minimumTimeOutMiliSec;
            public ComputationCallback callback;
            public GenericAi ai;
        }

        private List<Computation> _computation;

        public GenericAiPack()
        {
            Schedule();
        }

        ~GenericAiPack()
        {
            Unschedule();
        }

        public void Compute(GenericAi ai, ComputationCallback callback, long minimumTimeoutMiliSec = 0)
        {
            var comp = new Computation
            {
                ai = ai,
                callback = callback,
                minimumTimeOutMiliSec = minimumTimeoutMiliSec,
                shouldStop = false,
            };
            _computation.Add(comp);
        }

        public void Stop()
        {
            _computation.ForEach(x => x.shouldStop = true);
        }

        private void UpdateComputation(long miliSec)
        {
            for (int i = _computation.Count - 1; i >= 0; i--)
            {
                var comp = _computation[i];
                bool shouldRemove = comp.shouldStop;
                if (!shouldRemove && comp.ai.ElapsedTime > comp.minimumTimeOutMiliSec)
                {
                    var act = comp.ai.GetComputeMove();
                    if (act != null)
                    {
                        comp.callback(act);
                        shouldRemove = true;
                    }
                }
                if (shouldRemove)
                {
                    _computation.RemoveAt(i);
                }
            }
        }

        private void Schedule()
        {
            ControllerManager.Instance.Scheduler.Schedule("generic_ai_pack", UpdateComputation);
        }

        private void Unschedule()
        {
            ControllerManager.Instance.Scheduler.Unschedule("generic_ai_pack");
        }
    }
}
