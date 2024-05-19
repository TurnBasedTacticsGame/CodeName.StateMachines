using JetBrains.Annotations;

namespace CodeName.Core.Battles.StateMachines
{
    public delegate void StateChangedEvent<TState>([CanBeNull] TState previousState, [NotNull] TState newState) where TState : IState;
}
