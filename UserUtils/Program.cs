using DataUtils;

namespace UserUtils
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IOUtils.HelloMessage();
            while (true)
            {
                List<Patient>? patients = IOUtils.ReadData();
                if (patients is null)
                {
                    continue;
                }
            }
        }
    }
}