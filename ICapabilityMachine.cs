using System.Collections.Generic;
using Exanite.Core.Collections;

namespace CodeName.StateMachines
{
    public interface ICapabilityMachine<TCapability> where TCapability : ICapability
    {
        public IReadOnlyCollection<TCapability> DefaultCapabilities { get; }
        public IReadOnlyCollection<TCapability> EnabledCapabilities { get; }

        public bool IsCapabilityEnabled<T>() where T : TCapability;
        public bool TryGetEnabledCapability<T>(out T capability) where T : TCapability;

        public void SetCapability(TCapability capability, bool isEnabled);
    }

    public interface ICapabilityMachine<TKey, TCapability> : ICapabilityMachine<TCapability> where TCapability : ICapability
    {
        public IReadOnlyTwoWayDictionary<TKey, TCapability> RegisteredCapabilities { get; }
    }
}
