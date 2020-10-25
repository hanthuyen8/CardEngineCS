using System;
using System.Collections.Generic;

namespace engine.core
{
    using Callback = Action<object>;

    sealed public class StateListener
    { 
        public int Id { get; }

        private Callback _callback;

        public StateListener(int id, Callback callback)
        {
            Id = id;
            _callback = callback;
        }

        public void OnEvent(object arg)
        {
            _callback?.Invoke(arg);
        }
    }

    sealed public class StateListenerList
    {
        private List<StateListener> _listeners = new List<StateListener>();

        public StateListener AddListener(Callback callback, int priority = 0)
        {
            var listener = new StateListener(0, callback);
            return AddListener(listener, priority);
        }

        public StateListener AddListener(StateListener listener, int priority)
        {
            // FIXME: chưa làm đăng ký event

            _listeners.Add(listener);
            return listener;
        }

        public void Clear()
        {
            foreach (var lis in _listeners)
            { 
                // FIXME: chưa làm hủy đăng ký event
            }
            _listeners.Clear();
        }
    }
}
