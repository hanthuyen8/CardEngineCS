using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace engine.core
{

    sealed public class EventDispatcher
    {
        private bool _isDispatching;

        List<StateListener> _listeners;
        Queue<StateEvent> _events;

        public void AddListener(StateListener listener, int priority = 0)
        {
            if (_isDispatching || listener == null || IsRegistered(listener))
                return;

            _listeners.Insert(priority, listener);
        }

        public void RemoveListener(StateListener listener)
        {
            if (_isDispatching)
                return;

            _listeners.Remove(listener);
        }

        public bool IsRegistered(StateListener listener)
        {
            return _listeners.Find(x => x == listener) != null;
        }

        public StateEvent GetNextEvent()
        {
            if (_events.Count == 0)
                return null;

            return _events.Dequeue();
        }

        public void DispatchEvents(StateEvent ev)
        {
            _isDispatching = true;
            foreach (var lis in _listeners)
            {
                if (ev.CanDispatch(lis))
                {
                    ev.Dispatch(lis);
                }
            }
            _isDispatching = false;
        }

        public void DispatchEvents(Predicate<StateEvent> predicator = null)
        {
            if (predicator == null)
                return;

            EnumerateEvents((ev) => {
                if (!predicator(ev))
                    return false;

                DispatchEvents(ev);
                return true;
            });
        }

        public void PushEvent(StateEvent ev)
        {
            if (ev == null)
                return;

            _events.Enqueue(ev);
        }

        public void PushEvents(List<StateEvent> ev)
        {
            foreach (var e in ev)
            {
                if (e != null)
                    _events.Enqueue(e);
            }
        }

        public void ClearEvents(Predicate<StateEvent> predicator = null)
        {
            if (_isDispatching || predicator == null)
                return;

            EnumerateEvents((ev) => predicator(ev));
        }

        public void EnumerateEvents(Predicate<StateEvent> enumerator)
        {
            while (_events.Count > 0)
            {
                var ev = _events.Peek();
                if (!enumerator(ev))
                {
                    break;
                }
                _events.Dequeue();
            }
        }
    }
}
