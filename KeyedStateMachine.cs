using System;
using System.Collections.Generic;
using Exanite.Core.Collections;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeName.Core.Battles.StateMachines
{
    [Serializable]
    public class KeyedStateMachine<TKey, TState> : StateMachine<TState>, IStateMachine<TKey, TState>, ISerializationCallbackReceiver where TState : class, IState
    {
        [HideInInspector]
        [SerializeField] private List<RegisteredState> serializedRegisteredStates = new();

        [ShowInInspector]
        [PropertyOrder(20)]
        private TwoWayDictionary<TKey, TState> registeredStates = new();

        public IReadOnlyTwoWayDictionary<TKey, TState> RegisteredStates => registeredStates;

        public KeyedStateMachine() {}

        public KeyedStateMachine(TState initialState) : base(initialState) {}

        public override void ForceSetState(TState state)
        {
            if (state == null)
            {
                throw new ArgumentNullException(nameof(state));
            }

            if (!registeredStates.Inverse.ContainsKey(state))
            {
                throw new InvalidOperationException("State is not registered");
            }

            base.ForceSetState(state);
        }

        public void OnBeforeSerialize()
        {
            serializedRegisteredStates.Clear();
            foreach (var (key, state) in registeredStates)
            {
                serializedRegisteredStates.Add(new RegisteredState(key, state));
            }
        }

        public void OnAfterDeserialize()
        {
            registeredStates.Clear();
            foreach (var entry in serializedRegisteredStates)
            {
                registeredStates.Add(entry.Key, entry.State);
            }
        }

        [Serializable]
        private struct RegisteredState
        {
            [SerializeField] private TKey key;
            [SerializeField] private TState state;

            public RegisteredState(TKey key, TState state)
            {
                this.key = key;
                this.state = state;
            }

            public TKey Key
            {
                get => key;
                set => key = value;
            }

            public TState State
            {
                get => state;
                set => state = value;
            }
        }
    }
}
