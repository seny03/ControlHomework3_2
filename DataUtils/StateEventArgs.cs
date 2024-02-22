namespace DataUtils
{
    public class StateEventArgs : EventArgs
    {
        public bool PreviousState { get; }
        public bool CurrentState { get; }

        public StateEventArgs(bool previousState, bool currentState)
        {
            PreviousState = previousState;
            CurrentState = currentState;
        }
    }
}
