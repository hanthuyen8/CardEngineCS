using System;
using System.Collections.Generic;

namespace engine.core
{
    using Callback = Action<object>;

    sealed public class StateEvent
    {
        public object Data { get; }
        public int Id { get; }

        public StateEvent(object data)
        {
            Data = data;
        }

        public void Dispatch(StateListener listener)
        {
            listener?.OnEvent(Data);
        }

        public bool CanDispatch(StateListener listener)
        {
            return listener != null;
        }
    }
}
