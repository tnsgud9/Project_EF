using System;

namespace Collections
{
    public interface IState<TController>
    {
        void Start(TController controller);
        void Update(TController controller);
        void End(TController controller);
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
                if (_currentState is not null) _currentState.End(_controller);
                _currentState = value;
                _currentState.Start(_controller);
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

        public void Start(TController controller)
        {
            throw new NotImplementedException();
        }

        public void Update(TController controller)
        {
            _handleAction.Invoke();
        }

        public void End(TController controller)
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