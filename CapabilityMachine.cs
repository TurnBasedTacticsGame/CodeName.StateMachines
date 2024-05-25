using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeName.StateMachines
{
    [Serializable]
    public class CapabilityMachine<TCapability> : ICapabilityMachine<TCapability> where TCapability : class, ICapability
    {
        [PropertyOrder(0)]
        [SerializeField] private List<TCapability> defaultCapabilities = new();

        private HashSet<TCapability> enabledCapabilities = new();

        public IReadOnlyCollection<TCapability> DefaultCapabilities => defaultCapabilities;
        public IReadOnlyCollection<TCapability> EnabledCapabilities => enabledCapabilities;

        public void Initialize()
        {
            // Ensure that capabilities are enabled and disabled at least once so that initialization logic can run
            this.EnableAllCapabilities();
            this.DisableAllCapabilities();

            this.EnableDefaultCapabilities();
        }

        public bool IsCapabilityEnabled<T>() where T : TCapability
        {
            return TryGetEnabledCapability<T>(out _);
        }

        public bool TryGetEnabledCapability<T>(out T capability) where T : TCapability
        {
            foreach (var enabledCapability in enabledCapabilities)
            {
                if (enabledCapability is T typedCapability)
                {
                    capability = typedCapability;

                    return true;
                }
            }

            capability = default;

            return false;
        }

        public void EnableCapability(TCapability capability)
        {
            if (enabledCapabilities.Add(capability))
            {
                capability.OnEnableCapability();
            }
        }

        public void DisableCapability(TCapability capability)
        {
            if (enabledCapabilities.Remove(capability))
            {
                capability.OnDisableCapability();
            }
        }
    }
}
