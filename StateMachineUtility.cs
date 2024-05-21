using System;

namespace CodeName.StateMachines
{
    public static class StateMachineUtility
    {
        public static void AssertSetState<TState>(this IStateMachine<TState> machine, TState state) where TState : IState
        {
            if (!machine.TrySetState(state))
            {
                throw new Exception("Failed to set state");
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

        public static void ForceSetDefaultState<TState>(this IStateMachine<TState> machine) where TState : IState
        {
            if (machine.DefaultState == null)
            {
                throw new InvalidOperationException("Cannot force set default state. State machine does not have a default state set");
            }

            machine.ForceSetState(machine.DefaultState);
        }

        public static void AssertSetDefaultState<TState>(this IStateMachine<TState> machine) where TState : IState
        {
            if (machine.DefaultState == null)
            {
                throw new InvalidOperationException("Cannot force set default state. State machine does not have a default state set");
            }

            machine.AssertSetState(machine.DefaultState);
        }
    }
}
