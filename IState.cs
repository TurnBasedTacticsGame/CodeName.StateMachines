namespace CodeName.StateMachines
{
    public interface IState
    {
        public bool CanEnterState { get; }
        public bool CanExitState { get; }

        public void OnEnterState();
        public void OnExitState();
    }
}
