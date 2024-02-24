using DataUtils;

namespace UserUtils
{
    /// <summary>
    /// Предоставляет главную страницу меню для работы с пользователем.
    /// </summary>
    internal class MainPage : MenuPage
    {
        protected override Action[] Actions
        {
            get;
        }
        protected override string[] Descriptions
        {
            get;
        }

        internal MainPage(string[]? heading, List<Patient>? patients) : base(heading, patients)
        {
            // Переопределяем доступные в меню возможности.
            Descriptions = new string[]{
                "Отфильтровать данные по одному из полей",
                "Отсортировать данные по одному из полей",
                "Отредактировать значение поля в одном из объектов",
                "Выйти из программы"
            };

            Actions = new Action[]
            {
                FilterByProperty, SortByProperty, ChangePropertyValue, Exit
            };
        }
        internal MainPage()
        {
            // Переопределяем доступные в меню возможности.
            Descriptions = new string[]{
                "Отфильтровать данные по одному из полей",
                "Отсортировать данные по одному из полей",
                "Отредактировать значение поля в одном из объектов",
                "Выйти из программы"
            };

            Actions = new Action[]
            {
                FilterByProperty, SortByProperty, ChangePropertyValue, Exit
            };
        }

        // Следующие методы реализуют соответствующий условию и доступный для выбора в меню функционал.
        private void FilterByProperty()
        {
            FullMenu.CurrentPage = new FilterPage(Heading, Patients);
        }
        private void SortByProperty()
        {
            FullMenu.CurrentPage = new SortPage(Heading, Patients);
        }
        private void ChangePropertyValue()
        {
            int? patientId = IOUtils.GetPropertyId(Patients);
            if (patientId is null)
            {
                return;
            }
            IOUtils.PrintTable(Heading, DataProcessing.Filter(Patients, "patient_id", patientId).ToArray());
            IOUtils.Plug();
            FullMenu.CurrentPage = new ChangeValuePage(Heading, Patients, (int)patientId);
        }
        private void Exit() => Environment.Exit(0);
    }
}

