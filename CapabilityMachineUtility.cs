using UnityEngine.Pool;

namespace CodeName.StateMachines
{
    public static class CapabilityMachineUtility
    {
        public static void Reset<TCapability>(this ICapabilityMachine<TCapability> capabilityMachine) where TCapability : ICapability
        {
            capabilityMachine.DisableAllCapabilities();
            capabilityMachine.EnableDefaultCapabilities();
        }

        public static void ResetOnStateChanged<TCapability, TState>(
            this ICapabilityMachine<TCapability> capabilityMachine,
            IStateMachine<TState> stateMachine)
            where TCapability : ICapability
            where TState : IState
        {
            stateMachine.StateChangeInProgress += (_, _) =>
            {
                capabilityMachine.Reset();
            };
        }

        public static void SetAllCapabilities<TCapability>(this ICapabilityMachine<TCapability> machine, bool isEnabled) where TCapability : ICapability
        {
            using var listHandle = ListPool<TCapability>.Get(out var capabilities);
            capabilities.AddRange(machine.EnabledCapabilities);

            foreach (var capability in capabilities)
            {
                machine.SetCapability(capability, isEnabled);
            }
        }

        public static void DisableAllCapabilities<TCapability>(this ICapabilityMachine<TCapability> machine) where TCapability : ICapability
        {
            machine.SetAllCapabilities(false);
        }

        public static void EnableAllCapabilities<TCapability>(this ICapabilityMachine<TCapability> machine) where TCapability : ICapability
        {
            machine.SetAllCapabilities(true);
        }

        public static void EnableDefaultCapabilities<TCapability>(this ICapabilityMachine<TCapability> machine) where TCapability : ICapability
        {
            using var listHandle = ListPool<TCapability>.Get(out var capabilities);
            capabilities.AddRange(machine.DefaultCapabilities);

            foreach (var capability in capabilities)
            {
                machine.EnableCapability(capability);
            }
        }

        public static void EnableCapability<TCapability>(this ICapabilityMachine<TCapability> machine, TCapability capability) where TCapability : ICapability
        {
            machine.SetCapability(capability, true);
        }

        public static void DisableCapability<TCapability>(this ICapabilityMachine<TCapability> machine, TCapability capability) where TCapability : ICapability
        {
            machine.SetCapability(capability, false);
        }
    }
}
