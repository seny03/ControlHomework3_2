using DataUtils;

namespace UserUtils
{
    /// <summary>
    /// Предоставляет страницу меню для фильтрации данных.
    /// </summary>
    internal class FilterPage : MenuPage
    {
        protected override Action[] Actions
        {
            get;
        }
        protected override string[] Descriptions
        {
            get;
        }

        internal FilterPage(string[]? heading, List<Patient>? patients) : base(heading, patients)
        {
            Descriptions = Patient.Properties;
            List<Action> actions = new();
            foreach (var property in Patient.Properties)
            {
                actions.Add(() => Filter(property));
            }
            Actions = actions.ToArray();
        }
        internal FilterPage() : this(FullMenu.CurrentPage.Heading, FullMenu.CurrentPage.Patients) { }

        private void Filter(string property)
        {
            FullMenu.CurrentPage = new MainPage(Heading, IOUtils.Filter(Patients, property));
        }
    }
}
