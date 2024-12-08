using System;

namespace Collections
{
    public interface IState<TController>
    {
        void StateStart(TController controller);
        void StateUpdate(TController controller);
        void StateEnd(TController controller);
    }

    public class StateContext<TController>
    {
        private readonly TController _controller;
        private IState<TController> _currentState;

        public StateContext(TController controller)
        {
            _controller = controller;
        }

        public IState<TController> CurrentState
        {
            get => _currentState;
            set
            {
                if (_currentState == value) return;
                if (_currentState is not null) _currentState.StateEnd(_controller);
                _currentState = value;
                _currentState.StateStart(_controller);
            }
        }
    }

    public class State<TController> : IState<TController>
    {
        private readonly Action _endAction;
        private readonly Action _handleAction;
        private readonly Action _startAction;

        public State(Action startAction, Action handleAction, Action endAction)
        {
            _startAction = startAction;
            _handleAction = handleAction;
            _endAction = endAction;
        }

        public void StateStart(TController controller)
        {
            throw new NotImplementedException();
        }

        public void StateUpdate(TController controller)
        {
            _handleAction.Invoke();
        }

        public void StateEnd(TController controller)
        {
            throw new NotImplementedException();
        }

        public void Start()
        {
            _startAction.Invoke();
        }

        public void End()
        {
            _endAction.Invoke();
        }
    }
}