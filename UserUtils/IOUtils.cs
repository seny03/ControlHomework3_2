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
        internal static void ConsoleClear()
        {
            Console.Clear();
            Console.WriteLine("\x1b[3J");
            Console.Clear();
        }

        /// <summary>
        /// Выводит данные в табличном виде с заголовками.
        /// </summary>
        /// <param name="heading">Заголовки.</param>
        /// <param name="data">Данные.</param>
        /// <returns></returns>
        public static bool PrintTable(string[]? heading, Apartment[]? data)
        {
            if (data is null)
            {
                Console.WriteLine("Таблица пустая.");
                return false;
            }

            string?[][] rows = new string[data.Length][];
            for (int i = 0; i < data.Length; i++)
            {
                rows[i] = data[i].ToArray();
            }

            if (heading is null || rows is null || heading.Length == 0 || rows.Length == 0)
            {
                Console.WriteLine("Таблица пустая.");
                return false;
            }

            // Проверяем, что во всех строках одно и то же количество столбцов.
            var formatException = new FormatException("Количество столбцов должно совпадать для всех заголовков и строк с данными.");
            if (heading.Length != rows[0].Length)
            {
                throw formatException;
            }
            for (int i = 0; i < rows.Length - 1; i++)
            {
                if (rows[i].Length != rows[i + 1].Length)
                {
                    throw formatException;
                }
            }

            // Для каждого столбца запоминаем максимальную длину, занимаемую в данных.
            int columnsNumber = heading.Length;
            var columnsLength = new int[columnsNumber];
            // Оставляем только непустые столбцы.
            var isShownColumn = new bool[columnsNumber];

            for (int i = 0; i < columnsNumber; i++)
            {
                columnsLength[i] = heading[i].Length;
            }
            for (int i = 0; i < rows.Length; i++)
            {
                for (int j = 0; j < columnsNumber; j++)
                {
                    columnsLength[j] = Math.Max(columnsLength[j], rows[i][j].Length);
                    if (!String.IsNullOrEmpty(rows[i][j].Trim()))
                    {
                        isShownColumn[j] = true;
                    }
                }
            }

            var paddedHeading = heading[..];
            for (int j = 0; j < paddedHeading.Length; j++)
            {
                paddedHeading[j] = paddedHeading[j].PadRight(columnsLength[j]);
            }
            int curIndex = 0;
            for (int j = 0; j < paddedHeading.Length; j++)
            {
                if (isShownColumn[j])
                {
                    paddedHeading[curIndex++] = paddedHeading[j];
                }
            }
            int allPaddingLength = 0;
            for (int j = 0; j < columnsNumber; j++)
            {
                if (isShownColumn[j])
                {
                    allPaddingLength += columnsLength[j];
                }
            }

            string separatorString = new String('-', Math.Min(s_maxSymbolsInWidth, allPaddingLength + 3 * (isShownColumn.Sum(x => Convert.ToInt32(x)) - 1)));
            Console.WriteLine(separatorString);

            Array.Resize(ref paddedHeading, curIndex);
            string headingString = String.Join(" | ", paddedHeading);
            Console.WriteLine(headingString[..Math.Min(s_maxSymbolsInWidth, headingString.Length)]);

            Console.WriteLine(separatorString);

            for (int i = 0; i < rows.Length; i++)
            {
                var paddedRows = rows[i][..];
                for (int j = 0; j < paddedRows.Length; j++)
                {
                    paddedRows[j] = paddedRows[j].PadRight(columnsLength[j]);
                }
                curIndex = 0;
                for (int j = 0; j < paddedRows.Length; j++)
                {
                    if (isShownColumn[j])
                    {
                        paddedRows[curIndex++] = paddedRows[j];
                    }
                }
                Array.Resize(ref paddedRows, curIndex);
                string curRow = String.Join(" | ", paddedRows);
                Console.WriteLine(curRow[..Math.Min(s_maxSymbolsInWidth, curRow.Length)]);
            }
            Console.WriteLine(separatorString);
            Console.WriteLine();
            return true;
        }
    }
}
