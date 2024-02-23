using DataUtils;

namespace UserUtils
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IOUtils.HelloMessage();
            Patient p = new(1, "", 10, "m", "", 10, 10, 10, null);
        }
    }
}