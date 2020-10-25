using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace engine.core
{
    sealed public class Scheduler
    {
        private class CallbackInfo
        {
            public long delayMiliSec;
            public uint loops;
            public Action<long> callback;
        }

        private bool _isProcessing;
        private Dictionary<string, CallbackInfo> _callbacks = new Dictionary<string, CallbackInfo>();
        private Dictionary<string, CallbackInfo> _toBeAdded = new Dictionary<string, CallbackInfo>();
        private List<string> _toBeRemoved = new List<string>();

        public void ScheduleOnce(string key, Action<long> callback, long delayMiliSec = 0)
        {
            Schedule(key, callback, delayMiliSec, 1);
        }

        public void Schedule(string key, Action<long> callback, long delayMiliSec = 0, uint loops = uint.MaxValue)
        {
            if (_callbacks.ContainsKey(key) || _toBeAdded.ContainsKey(key))
                return;

            var info = new CallbackInfo
            {
                callback = callback,
                delayMiliSec = delayMiliSec,
                loops = loops
            };

            if (_isProcessing)
                _toBeAdded[key] = info;
            else
                _callbacks[key] = info;
        }

        public void Unschedule(string key)
        {
            if (!_callbacks.ContainsKey(key) || !_toBeAdded.ContainsKey(key))
                return;

            if (_isProcessing)
            {
                _toBeRemoved.Add(key);
            }
            else
            {
                if (_callbacks.ContainsKey(key))
                    _callbacks.Remove(key);
                else if (_toBeAdded.ContainsKey(key))
                    _callbacks.Remove(key);
            }
        }

        public void Process(long deltaMiliSec)
        {
            Flush();

            _isProcessing = true;
            foreach (var cb in _callbacks)
            {
                var info = cb.Value;

                if (info.delayMiliSec <= deltaMiliSec)
                {
                    info.delayMiliSec = 0;
                    --info.loops;
                    info.callback?.Invoke(deltaMiliSec);
                    if (info.loops == 0)
                        Unschedule(cb.Key);
                }
                else
                {
                    info.delayMiliSec -= deltaMiliSec;
                }
            }
            _isProcessing = false;
        }

        private void Flush()
        {
            foreach (var cb in _toBeAdded)
            {
                _callbacks[cb.Key] = cb.Value;
            }
            _toBeAdded.Clear();

            foreach (var key in _toBeRemoved)
            {
                _callbacks.Remove(key);
            }
            _toBeRemoved.Clear();
        }
    }
}
