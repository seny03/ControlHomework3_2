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
        internal static List<Patient>? ReadData()
        {
            Console.Write("Введите путь для считывания данных: ");
            string? path = Console.ReadLine();
            try
            {
                List<Patient>? patients = JsonParser.ReadJson(path);
                AutoSaver autoSaver = new AutoSaver(path, patients);
                return patients;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"При попытке считать данные из файла возникла ошибка: {ex.Message}");
            }
            return null;
        }
    }
}
