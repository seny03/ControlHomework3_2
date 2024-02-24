using DataUtils;

namespace UserUtils
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IOUtils.HelloMessage();

            // В методах классов я выбрасываю исключения, поэтому тут будем их ловить.
            while (true)
            {
                try
                {
                    List<Patient>? patients = IOUtils.ReadData();
                    if (patients is null)
                    {
                        continue;
                    }
                    FullMenu.CurrentPage = new MainPage(Patient.Properties, patients);
                    FullMenu.LoadMenu();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"В процессе выполнения возникла ошибка: " + ex.Message);
                }
            }
        }
    }
}