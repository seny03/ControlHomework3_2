namespace DataUtils
{
    public class UpdatedEventArgs : EventArgs
    {
        public DateTime ChangeDateTime { get; }

        public UpdatedEventArgs(DateTime changeDateTime)
        {
            ChangeDateTime = changeDateTime;
        }
    }
}
