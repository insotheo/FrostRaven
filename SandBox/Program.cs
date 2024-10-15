using FrostRaven.Core;

namespace SandBox
{
    class Program
    {
        private static void Main()
        {
            Log.DefaultSender = "SandBox";
            Log.Trace("Hello, World!");
            using(MyGame game = new MyGame())
            {
                game.Run();
            }
        }
    }
}