using DataUtils;

namespace UserUtils
{
    internal static class IOUtils
    {
        /// <summary>
        /// Приветственное сообщение в начале работы программы.
        /// </summary>
        internal static void HelloMessage() =>
            Console.WriteLine($"Привет {Environment.UserName}! Данная программма дает возможность считать данные из json файла и выполнить над ними описанные в условии действия. " +
                "Внимание! Структура json файла должна полностью совпадать с описанной в условии (иметь те же заголовки, разделители и т.д.)." +
                Environment.NewLine);

        /// <summary>
        /// Ведет диалог с пользователем и считывает json данные.
        /// </summary>
        internal static Patient[] ReadData()
        {
            Console.Write("Введите путь для считывания данных:");
            string? path = Console.ReadLine();
            try
            {
                using StreamReader input = new StreamReader(path);
                {
                    Console.SetIn(input);
                    return JsonParser.ReadJson();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"При попытке считать данные из файла возникла ошибка: {ex.Message}");
                Console.WriteLine("Считывание данных будет осуществлятся через консоль; чтобы прекратить ввод данных, наберите Ctrl + Z и нажмите Enter:");
            }
            return JsonParser.ReadJson();
        }
    }
}
