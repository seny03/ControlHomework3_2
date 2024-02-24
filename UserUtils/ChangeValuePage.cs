using DataUtils;

namespace UserUtils
{
    /// <summary>
    /// Предоставляет страницу меню для сортировки данных.
    /// </summary>
    internal class ChangeValuePage : MenuPage
    {
        protected override Action[] Actions
        {
            get;
        }
        protected override string[] Descriptions
        {
            get;
        }

        internal ChangeValuePage(string[]? heading, List<Patient>? patients, int patientId) : base(heading, patients)
        {
            Descriptions = Patient.ChangeableProperties;
            List<Action> actions = new();
            foreach (var property in Patient.ChangeableProperties)
            {
                actions.Add(() => ChangeValue(patientId, property));
            }
            Actions = actions.ToArray();
        }
        internal ChangeValuePage(int patientId)
        {
            Descriptions = Patient.ChangeableProperties;
            List<Action> actions = new();
            foreach (var property in Patient.ChangeableProperties)
            {
                actions.Add(() => ChangeValue(patientId, property));
            }
            Actions = actions.ToArray();
        }

        private void ChangeValue(int patientId, string property)
        {
            FullMenu.CurrentPage = new MainPage(Heading, IOUtils.ChangePropertyValue(Patients, patientId, property));
        }
    }
}
