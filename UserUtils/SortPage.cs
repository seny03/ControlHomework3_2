using DataUtils;

namespace UserUtils
{
    /// <summary>
    /// Предоставляет страницу меню для сортировки данных.
    /// </summary>
    internal class SortPage : MenuPage
    {
        protected override Action[] Actions
        {
            get;
        }
        protected override string[] Descriptions
        {
            get;
        }

        internal SortPage(string[]? heading, List<Patient>? patients) : base(heading, patients)
        {
            Descriptions = Patient.Properties;
            List<Action> actions = new();
            foreach (var property in Patient.Properties)
            {
                actions.Add(() => Sort(property));
            }
            Actions = actions.ToArray();
        }

        internal SortPage() : this(FullMenu.CurrentPage.Heading, FullMenu.CurrentPage.Patients) { }

        private void Sort(string property)
        {
            FullMenu.CurrentPage = new MainPage(Heading, IOUtils.Sort(Patients, property));
        }
    }
}
