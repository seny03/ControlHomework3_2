using DataUtils;

namespace UserUtils
{
    /// <summary>
    /// Предоставляет методы для работы с пользователем через консоль.
    /// </summary>
    internal static class IOUtils
    {
        // Максимальное количесто символов в ширину при выводе в консоль.
        private static int s_maxSymbolsInWidth = 155;

        /// <summary>
        /// Приветственное сообщение в начале работы программы.
        /// </summary>
        internal static void HelloMessage() =>
            Console.WriteLine($"Привет {Environment.UserName}! Данная программма дает возможность считать данные из json файла и выполнить над ними описанные в условии действия. " +
                "Внимание! Структура json файла должна полностью совпадать с описанной в условии (иметь те же заголовки, разделители и т.д.)." +
                Environment.NewLine);
        /// <summary>
        /// Предлагает нажать любую клавишу и ожидает ее нажатие.
        /// </summary>
        internal static void Plug()
        {
            Console.Write("Нажмите любую клавишу, чтобы продолжить: ");
            Console.ReadKey();
        }

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
                AutoSaver autoSaver = new AutoSaver(path);
                return patients;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"При попытке считать данные из файла возникла ошибка: {ex.Message}");
            }
            return null;
        }
        /// <summary>
        /// Полностью очищает консоль.
        /// </summary>
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
        public static bool PrintTable(string[]? heading, Patient[]? data)
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

        internal static List<Patient> Filter(List<Patient>? data, string property)
        {
            Console.Write($"Введите значение поля \"{property}\", по которому будет осуществлятся фильтрация: ");
            string? value = Console.ReadLine();
            List<Patient> filteredData = DataProcessing.Filter(data, property, value);
            PrintTable(Patient.Properties, filteredData.ToArray());
            Plug();
            return filteredData;
        }
        internal static List<Patient> Sort(List<Patient>? data, string property)
        {
            List<Patient> sortedData = DataProcessing.Sort(data, property);
            PrintTable(Patient.Properties, sortedData.ToArray());
            Plug();
            return sortedData;
        }
        internal static int? GetPropertyId(List<Patient>? patients)
        {
            if (patients is null || patients.Count == 0)
            {
                Console.WriteLine("Невозможно выполнить изменение какого-либо поля, потому что таблица с данными пустая.");
                return null;
            }
            while (true)
            {
                Console.Write("Введите значение \"patient_id\" объекта, поле которого нужно изменить: ");
                if (int.TryParse(Console.ReadLine(), out int patientId))
                {
                    if (patients.Any(x => x.PatientId == patientId))
                    {
                        return patientId;
                    }
                    Console.WriteLine($"В данных не содержится пациент с значением \"patient_id\" = {patientId}");
                }
                Console.WriteLine("Введенное значение некорректно.");
            }
        }
        internal static List<Patient> ChangePropertyValue(List<Patient> data, int patinetId, string property)
        {
            while (true)
            {
                Console.Write($"Введите новое значение свойства \"{property}\" для пациента с \"patient_id\" = {patinetId}: ");
                object? value = Console.ReadLine();
                try
                {
                    foreach (var patient in DataProcessing.Filter(data, "patient_id", patinetId))
                    {
                        patient.ChangeField(property, value, data);
                    }
                    return data;
                }
                catch
                {
                    Console.WriteLine($"Некорректное значение для свойства \"{property}\".");
                }
            }
        }
    }
}
