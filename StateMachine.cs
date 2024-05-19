using System;
using JetBrains.Annotations;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeName.StateMachines
{
    [Serializable]
    public class StateMachine<TState> : IStateMachine<TState> where TState : class, IState
    {
        [PropertyOrder(0)]
        [SerializeField] private TState initialState;

        [PropertyOrder(1)]
        [SerializeField] private TState defaultState;

        private TState currentState;

        public TState InitialState
        {
            get => initialState;
            set => initialState = value;
        }

        public TState DefaultState
        {
            get => defaultState;
            set => defaultState = value;
        }

        [ShowInInspector]
        [PropertyOrder(10)]
        public TState CurrentState => currentState;

        public event StateChangedEvent<TState> StateChanging;
        public event StateChangedEvent<TState> StateChangeInProgress;
        public event StateChangedEvent<TState> StateChanged;

        public StateMachine() {}

        public StateMachine(TState initialState)
        {
            this.initialState = initialState;
        }

        public virtual void Initialize()
        {
            ForceSetState(initialState);
        }

        public bool TrySetState([NotNull] TState state)
        {
            if (state == null)
            {
                throw new ArgumentNullException(nameof(state));
            }

            if (state == currentState)
            {
                return true;
            }

            if (currentState.CanExitState && state.CanEnterState)
            {
                ForceSetState(state);

                return true;
            }

            return false;
        }

        public virtual void ForceSetState([NotNull] TState state)
        {
            if (state == null)
            {
                throw new ArgumentNullException(nameof(state));
            }

            var previousState = currentState;
            StateChanging?.Invoke(previousState, currentState);

            if (previousState != null)
            {
                previousState.OnExitState();
            }
            StateChangeInProgress?.Invoke(previousState, currentState);

            currentState = state;
            currentState.OnEnterState();
            StateChanged?.Invoke(previousState, currentState);
        }
    }
}
