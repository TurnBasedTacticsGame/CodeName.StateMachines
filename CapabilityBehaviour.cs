using System;
using UnityEngine;

namespace CodeName.StateMachines
{
    public class CapabilityBehaviour : MonoBehaviour, ICapability
    {
        public void OnEnableCapability()
        {
            if (gameObject.activeSelf)
            {
                throw new InvalidOperationException($"The GameObject that {GetType().Name} is on was already enabled when {nameof(OnEnableCapability)} was called");
            }

            gameObject.SetActive(true);
        }

        public void OnDisableCapability()
        {
            if (!gameObject.activeSelf)
            {
                throw new InvalidOperationException($"The GameObject that {GetType().Name} is on was already disabled when {nameof(OnDisableCapability)} was called");
            }

            gameObject.SetActive(false);
        }
    }
}
