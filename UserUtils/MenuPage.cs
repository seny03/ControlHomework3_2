using DataUtils;

namespace UserUtils
{
    /// <summary>
    /// Предоставляет возмонжость создавать страницы для пользовательского меню.
    /// </summary>
    internal abstract class MenuPage
    {
        internal bool iShow { get; set; } = true;

        protected virtual Action[] Actions
        {
            get;
        } = { };
        protected virtual string[] Descriptions
        {
            get;
        } = { };
        public string[]? Heading
        {
            get; set;
        }

        public List<Patient>? Patients
        {
            get; set;
        }

        private int currentOption = 0;
        public int CurrentOption
        {
            get { return currentOption; }
            set { currentOption = (value + Descriptions.Length) % Descriptions.Length; }
        }
        internal MenuPage(string[]? heading, List<Patient>? patients)
        {
            Heading = heading;
            Patients = patients;
        }
        internal MenuPage() { }

        /// <summary>
        /// Выводит страницу один раз.
        /// </summary>
        public void ShowPage()
        {
            IOUtils.ConsoleClear();

            for (int i = 0; i < Descriptions.Length; i++)
            {
                if (i == currentOption)
                {
                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine($"{i + 1}. " + Descriptions[i]);
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine($"{i + 1}. " + Descriptions[i]);
                }
            }
        }

        /// <summary>
        /// Запускает выполнение запроса в выбранном пункте меню и прекращает показывать меню для корректного отображения результата.
        /// </summary>
        internal void ActCurrentOption()
        {
            iShow = false;
            IOUtils.ConsoleClear();
            Actions[currentOption].Invoke();
        }
    }
}

