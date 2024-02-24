namespace DataUtils
{
    /// <summary>
    /// Предоставляет данные для события обновления какого-либо из полей пациента.
    /// </summary>
    public class UpdatedEventArgs : EventArgs
    {
        public DateTime ChangeDateTime { get; }
        public List<Patient> AllData { get; }

        public UpdatedEventArgs(DateTime changeDateTime, List<Patient> allData)
        {
            ChangeDateTime = changeDateTime;
            AllData = allData;
        }
    }
}
