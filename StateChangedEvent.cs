using JetBrains.Annotations;

namespace CodeName.StateMachines
{
    public delegate void StateChangedEvent<TState>([CanBeNull] TState previousState, [NotNull] TState newState) where TState : IState;
}
