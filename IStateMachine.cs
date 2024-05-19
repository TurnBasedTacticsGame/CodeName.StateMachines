using Exanite.Core.Collections;

namespace CodeName.Core.Battles.StateMachines
{
    public interface IStateMachine<TState> where TState : IState
    {
        public TState InitialState { get; set; }
        public TState DefaultState { get; set; }
        public TState CurrentState { get; }

        public bool TrySetState(TState state);
        public void ForceSetState(TState state);

        /// <summary>
        /// Called before the previous state is exited.
        /// </summary>
        public event StateChangedEvent<TState> StateChanging;

        /// <summary>
        /// Called after the previous state is exited AND before the new state is entered.
        /// </summary>
        public event StateChangedEvent<TState> StateChangeInProgress;

        /// <summary>
        /// Called after the new state is entered.
        /// </summary>
        public event StateChangedEvent<TState> StateChanged;
    }

    public interface IStateMachine<TKey, TState> : IStateMachine<TState> where TState : IState
    {
        public IReadOnlyTwoWayDictionary<TKey, TState> RegisteredStates { get; }
    }
}
