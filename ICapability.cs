namespace CodeName.Core.Battles.StateMachines
{
    public interface ICapability
    {
        public void OnEnableCapability();
        public void OnDisableCapability();
    }
}