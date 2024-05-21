using Exanite.Core.Collections;

namespace CodeName.StateMachines
{
    public interface IStateMachine<TState> where TState : IState
    {
        public TState DefaultState { get; set; }
        public TState CurrentState { get; }

        /// <summary>
        /// Attempts to transition to the new <see cref="state"/>.
        /// Returns true if the <see cref="CurrentState"/> was already set to <see cref="state"/>
        /// or it was changed to <see cref="state"/>.
        /// </summary>
        public bool TrySetState(TState state);

        /// <summary>
        /// Transitions to the new <see cref="state"/> without checking for <see cref="IState.CanEnterState"/> and <see cref="IState.CanExitState"/>.
        /// </summary>
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
