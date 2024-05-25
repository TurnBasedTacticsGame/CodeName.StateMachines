using System;

namespace CodeName.StateMachines
{
    public static class StateMachineUtility
    {
        public static TState AssertGetState<TKey, TState>(this IStateMachine<TKey, TState> machine, TKey key) where TState : IState
        {
            if (!machine.RegisteredStates.TryGetValue(key, out var state))
            {
                throw new InvalidOperationException("State is not registered");
            }

            return state;
        }

        public static bool TrySetState<TKey, TState>(this IStateMachine<TKey, TState> machine, TKey key) where TState : IState
        {
            return machine.TrySetState(machine.AssertGetState(key));
        }

        public static void AssertSetState<TKey, TState>(this IStateMachine<TKey, TState> machine, TKey key) where TState : IState
        {
            if (!machine.TrySetState(machine.AssertGetState(key)))
            {
                throw new InvalidOperationException("Failed to set state");
            }
        }

        public static void ForceSetState<TKey, TState>(this IStateMachine<TKey, TState> machine, TKey key) where TState : IState
        {
            machine.ForceSetState(machine.AssertGetState(key));
        }

        public static void AssertSetState<TState>(this IStateMachine<TState> machine, TState state) where TState : IState
        {
            if (!machine.TrySetState(state))
            {
                throw new InvalidOperationException("Failed to set state");
            }
        }

        public static bool TrySetDefaultState<TState>(this IStateMachine<TState> machine) where TState : IState
        {
            if (machine.DefaultState == null)
            {
                return false;
            }

            return machine.TrySetState(machine.DefaultState);
        }

        public static void AssertSetDefaultState<TState>(this IStateMachine<TState> machine) where TState : IState
        {
            if (machine.DefaultState == null)
            {
                throw new InvalidOperationException("Cannot force set default state. State machine does not have a default state set");
            }

            machine.AssertSetState(machine.DefaultState);
        }

        public static void ForceSetDefaultState<TState>(this IStateMachine<TState> machine) where TState : IState
        {
            if (machine.DefaultState == null)
            {
                throw new InvalidOperationException("Cannot force set default state. State machine does not have a default state set");
            }

            machine.ForceSetState(machine.DefaultState);
        }
    }
}
