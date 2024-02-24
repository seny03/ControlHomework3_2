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
        internal MainPage()
        {
            // Переопределяем доступные в меню возможности.
            Descriptions = new string[]{
                "Отфильтровать данные по одному из полей",
                "Отсортировать данные по одному из полей",
                "Выйти из программы"
            };

            Actions = new Action[]
            {
                FilterByProperty, SortByProperty, Exit
            };
        }

        // Следующие методы реализуют соответствующий условию и доступный для выбора в меню функционал.
        private void FilterByProperty()
        {
            var (property, value) = IOUtils.GetPropertyAndValue();
            Apartments = DataProcessing.Filter(Apartments, property, value);
            if (IOUtils.PrintTable(Heading, Apartments))
            {
                IOUtils.SaveData(Apartments);
            }
        }
        private void SortByProperty()
        {
            string property = IOUtils.GetProperty();
            Apartments = DataProcessing.Sort(Apartments, property);
            if (IOUtils.PrintTable(Heading, Apartments))
            {
                IOUtils.SaveData(Apartments);
                Console.SetOut(new StreamWriter(Console.OpenStandardOutput()));
            }
        }
        private void Exit() => Environment.Exit(0);
    }
}

