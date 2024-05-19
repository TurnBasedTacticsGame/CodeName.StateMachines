using System;
using UnityEngine;

namespace CodeName.Core.Battles.StateMachines
{
    public abstract class StateBehaviour : MonoBehaviour, IState
    {
        public virtual bool CanEnterState => true;
        public virtual bool CanExitState => true;

        public void OnEnterState()
        {
            if (gameObject.activeSelf)
            {
                throw new InvalidOperationException($"The GameObject that {GetType().Name} is on was already enabled when {nameof(OnEnterState)} was called");
            }

            gameObject.SetActive(true);
        }

        public void OnExitState()
        {
            if (!gameObject.activeSelf)
            {
                throw new InvalidOperationException($"The GameObject that {GetType().Name} is on was already disabled when {nameof(OnEnterState)} was called");
            }

            gameObject.SetActive(false);
        }
    }
}
