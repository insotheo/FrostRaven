using FrostRaven.Core;

namespace SandBox
{
    class Program
    {
        static void Main()
        {
            Log.DefaultSender = "SandBox";
            Log.Trace("Hello, World!");
        }
    }
}