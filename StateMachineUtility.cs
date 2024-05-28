using System;

namespace CodeName.StateMachines
{
    public static class StateMachineUtility
    {
        public static bool TrySetState<TKey, TState>(this IStateMachine<TKey, TState> machine, TKey key) where TState : IState
        {
            return machine.TrySetState(machine.RegisteredStates[key]);
        }

        public static void AssertSetState<TKey, TState>(this IStateMachine<TKey, TState> machine, TKey key) where TState : IState
        {
            machine.AssertSetState(machine.RegisteredStates[key]);
        }

        public static void ForceSetState<TKey, TState>(this IStateMachine<TKey, TState> machine, TKey key) where TState : IState
        {
            machine.ForceSetState(machine.RegisteredStates[key]);
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
                throw new InvalidOperationException("Cannot force set default state. State machine does not have a default state set");
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
