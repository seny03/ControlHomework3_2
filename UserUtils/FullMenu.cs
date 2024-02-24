namespace UserUtils
{
    /// <summary>
    /// Предоставляет меню с возможностью менять страницы для работы с пользователем.
    /// </summary>
    internal class FullMenu
    {
        static internal MenuPage CurrentPage
        {
            get; set;
        } = new MainPage();

        /// <summary>
        /// Начинает показ меню, пока не понадобится из него выйти.
        /// </summary>
        static public void LoadMenu()
        {
            CurrentPage.iShow = true;
            Console.CursorVisible = false;
            while (CurrentPage.iShow)
            {
                CurrentPage.ShowPage();
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.UpArrow:
                        CurrentPage.CurrentOption--;
                        break;
                    case ConsoleKey.DownArrow:
                        CurrentPage.CurrentOption++;
                        break;
                    case ConsoleKey.Enter:
                        CurrentPage.ActCurrentOption();
                        break;
                }
            }
            Console.CursorVisible = true;
        }
    }
}
