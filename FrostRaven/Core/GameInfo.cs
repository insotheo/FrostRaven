using Silk.NET.Windowing;
using System;

namespace FrostRaven.Core
{
    public unsafe static class GameInfo
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
            try
            {
                if (p_window == null)
                {
                    return -1;
                }
                return p_window->Time;
            }
            catch { return -1; }
        }

        public static void QuitGame()
        {
            try
            {
                if (p_window == null)
                {
                    return;
                }
                p_window->Close();
            }
            catch { Environment.Exit(0); }
        }

        internal static void Finish() => p_window = null;

    }
}
