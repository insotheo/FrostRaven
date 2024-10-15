using Silk.NET.Windowing;

namespace FrostRaven.Core
{
    public unsafe static class GameTime
    {
#pragma warning disable CS8500
        private static IWindow* p_window;

        internal static void Run(IWindow* window)
        {
            if(window == null)
            {
                return;
            }
            p_window = window;
        }
#pragma warning restore CS8500

        public static double GetElapsedTime()
        {
            if(p_window == null)
            {
                return -1;
            }
            return p_window->Time;
        }

        public static void Finish() => p_window = null;

    }
}
