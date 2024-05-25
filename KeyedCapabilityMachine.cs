using System;
using System.Collections.Generic;
using Exanite.Core.Collections;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeName.StateMachines
{
    [Serializable]
    public class KeyedCapabilityMachine<TKey, TCapability> : CapabilityMachine<TCapability>, ICapabilityMachine<TKey, TCapability>, ISerializationCallbackReceiver where TCapability : class, ICapability
    {
        [HideInInspector]
        [SerializeField] private List<RegisteredCapability> serializedRegisteredCapabilities = new();

        [ShowInInspector]
        [PropertyOrder(20)]
        private TwoWayDictionary<TKey, TCapability> registeredCapabilities = new();

        public IReadOnlyTwoWayDictionary<TKey, TCapability> RegisteredCapabilities => registeredCapabilities;

        public override void EnableCapability(TCapability capability)
        {
            if (capability == null)
            {
                throw new ArgumentNullException(nameof(capability));
            }

            if (!registeredCapabilities.Inverse.ContainsKey(capability))
            {
                throw new InvalidOperationException("Capability is not registered");
            }

            base.EnableCapability(capability);
        }

        public void OnBeforeSerialize()
        {
            serializedRegisteredCapabilities.Clear();
            foreach (var (key, state) in registeredCapabilities)
            {
                serializedRegisteredCapabilities.Add(new RegisteredCapability(key, state));
            }
        }

        public void OnAfterDeserialize()
        {
            registeredCapabilities.Clear();
            foreach (var entry in serializedRegisteredCapabilities)
            {
                try
                {
                    registeredCapabilities.Add(entry.Key, entry.Capability);
                }
                catch (ArgumentNullException e)
                {
                    Debug.LogError($"Deserialized a null capability with key '{entry.Key}': {e}");
                }
                catch (Exception e)
                {
                    Debug.LogError(e);
                }
            }
        }

        [Serializable]
        private struct RegisteredCapability
        {
            [SerializeField] private TKey key;
            [SerializeField] private TCapability capability;

            public RegisteredCapability(TKey key, TCapability capability)
            {
                this.key = key;
                this.capability = capability;
            }

            public TKey Key
            {
                get => key;
                set => key = value;
            }

            public TCapability Capability
            {
                get => capability;
                set => capability = value;
            }
        }
    }
}
